using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Background;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

public class CreatePayrollPeriodCommandHandler(
    IGenericRepository<PayrollType> _payrollTypeRepository,
    IGenericRepository<PayrollPeriod> _payrollPeriodRepository,
    IGenericRepository<PayrollDetail> _payrollDetailRepository,
    IGenericRepository<EmployeeInfo> _employeeRepository,
    IGenericRepository<PayrollPeriodStatus> _statusRepository,
    IPayrollBackgroundTaskService _backgroundTaskService,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollPeriodCommandHandler> _logger
) : ICommandHandler<CreatePayrollPeriodCommand, long>
{
   public async Task<Result<long>> Handle(CreatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
        var payrollType = await _payrollTypeRepository.GetByIdAsync(command.PayrollTypeId!.Value, cancellationToken);

        string periodName = payrollType != null ? $"PLA-{payrollType.Code!.Trim()}-{command.StartDate:yyyyMM}" : $"Periodo_{command.StartDate:yyyyMMdd}_{command.EndDate:yyyyMMdd}";

        _logger.LogInformation("🚀 Iniciando proceso de creación de Planilla para período: {PeriodName}", periodName);

        try
        {
            // 1. VALIDACIONES (SIN TRANSACCIÓN)
            var validationResult = await ValidatePayrollPeriod(command, periodName, cancellationToken);
            if (validationResult.IsFailure)
                return Result.Failure<long>(validationResult.Error);

            // 2. TRANSACCIÓN CORTA: SOLO OPERACIONES CRÍTICAS
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 2.1 CREAR PERÍODO DE PLANILLA Y GUARDAR PARA OBTENER ID
            var payrollPeriod = await CreatePayrollPeriod(command, periodName, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken); // Aquí se genera el ID
            _logger.LogInformation("📅 Período de planilla creado: {PeriodId}", payrollPeriod.Id);

            // 2.2 CREAR DETALLES BÁSICOS (sin cálculos)
            var payrollDetails = await CreateBasicPayrollDetails(payrollPeriod.Id, cancellationToken);
            _logger.LogInformation("📋 Detalles básicos creados: {DetailsCount}", payrollDetails.Count);

            // 2.3 COMMIT RÁPIDO (sin cálculos complejos)
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("📥 Planilla {PayrollPeriodId} creada, iniciando procesamiento background", payrollPeriod.Id);

            // 3. ENCOLAR PROCESAMIENTO BACKGROUND (CÁLCULOS, ASIENTO CONTABLE, PAYMENT RECORDS)
            await _backgroundTaskService.QueuePayrollProcessingAsync(payrollPeriod.Id, command, periodName, cancellationToken);

            // 4. RESPUESTA INMEDIATA
            _logger.LogInformation("✅ Planilla con ID {PayrollPeriodId} creada satisfactoriamente, procesamiento en background iniciado", payrollPeriod.Id);
            
            return Result.Success(payrollPeriod.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "❌ Error al crear planilla para período {PeriodName}", periodName);
            return Result.Failure<long>(PayrollPeriodErrores.ErrorSave);
        }
    }

    private async Task<PayrollPeriod> CreatePayrollPeriod(CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken)
    {
        var statusId = await GetStatusId("BORRADOR", cancellationToken);

        var payrollPeriod = PayrollPeriod.Create(
            periodName,
            command.PayrollTypeId,
            command.StartDate,
            command.EndDate,
            0, // TotalAmount se actualizará en background
            0, // TotalEmployees se actualizará en background
            statusId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _payrollPeriodRepository.AddAsync(payrollPeriod, cancellationToken);
        return payrollPeriod;
    }

    private async Task<List<PayrollDetail>> CreateBasicPayrollDetails(long payrollPeriodId, CancellationToken cancellationToken)
    {
        var activeEmployees = await GetActiveEmployees(cancellationToken);
        var details = new List<PayrollDetail>();

        foreach (var employee in activeEmployees)
        {
            var detail = PayrollDetail.Create(
                payrollPeriodId,
                employee.Id,
                employee.CostCenterId,
                employee.BaseSalary / 2, // Salario base quincenal
                0, // TotalEarnings se calculará en background
                0, // TotalDeductions se calculará en background
                0, // NetPay se calculará en background
                await GetStatusId("BORRADOR", cancellationToken),
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                "system"
            );

            await _payrollDetailRepository.AddAsync(detail, cancellationToken);
            details.Add(detail);
        }

        return details;
    }

    private async Task<List<EmployeeInfoResponse>> GetActiveEmployees(CancellationToken cancellationToken)
    {
        /*var employees = await _employeeRepository.ListAsync(
            e => e.StateEmployeeInfo == (int)EstadosEnum.Activo,
            cancellationToken);*/

        BaseSpecParams SpecParams = new()
        {
            NoPaging = true,
            Sort = "employeeNameasc"
        };
        var spec = new EmployeeInfoSpecification(SpecParams);
        var employees = await _employeeRepository.ListAsync(spec, cancellationToken);
        //return employees.ToList();

        _logger.LogDebug("👥 {EmployeesCount} empleados activos encontrados", employees.Count);
        return employees.ToList();
    }

    private async Task<long> GetStatusId(string statusCode, CancellationToken cancellationToken)
    {
        var status = await _statusRepository.FirstOrDefaultAsync(
            s => s.Code == statusCode, cancellationToken);

        if (status == null)
        {
            _logger.LogWarning("⚠️ Estado no encontrado: {StatusCode}", statusCode);
            throw new Exception($"Estado no encontrado: {statusCode}");
        }

        return status.Id;
    }

    private async Task<Result> ValidatePayrollPeriod(CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken)
    {
        // Validar período duplicado
        bool existsPeriod = await _payrollPeriodRepository.ExistsAsync(
            p => p.PeriodName == periodName && p.PayrollTypeId == command.PayrollTypeId
            && p.StatePayrollPeriod == (int)EstadosEnum.Activo,
            cancellationToken);

        if (existsPeriod)
        {
            _logger.LogWarning("⚠️ Ya existe un período con el nombre: {PeriodName}", periodName);
            return Result.Failure(PayrollPeriodErrores.Duplicado);
        }

        // Validar fechas
        if (command.StartDate >= command.EndDate)
        {
            _logger.LogWarning("⚠️ Fechas inválidas: StartDate {StartDate} >= EndDate {EndDate}",
                command.StartDate, command.EndDate);
            return Result.Failure(PayrollPeriodErrores.FechasInvalidas);
        }

        _logger.LogDebug("✅ Validaciones de período exitosas");
        return Result.Success();
    }
}