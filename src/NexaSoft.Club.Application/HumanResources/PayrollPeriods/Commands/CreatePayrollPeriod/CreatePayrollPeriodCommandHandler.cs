using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.HumanResources.LegalParameters;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

public class CreatePayrollPeriodCommandHandler(
    IGenericRepository<PayrollPeriod> _payrollPeriodRepository,
    IGenericRepository<PayrollDetail> _payrollDetailRepository,
    IGenericRepository<PayrollDetailConcept> _payrollDetailConceptRepository,
    IGenericRepository<EmployeeInfo> _employeeRepository,
    IGenericRepository<AttendanceRecord> _attendanceRepository,
    IGenericRepository<PayrollConcept> _conceptRepository,
    IGenericRepository<PayrollPeriodStatus> _statusRepository,
    IGenericRepository<PayrollType> _payrollTypeRepository,
    ILegalParametersRepository _legalParameters,
    IPayrollCalculationService _payrollCalculationService,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollPeriodCommandHandler> _logger
) : ICommandHandler<CreatePayrollPeriodCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("🚀 Iniciando proceso de creación de Planilla para período: {PeriodName}", command.PeriodName);

        // 3. INICIAR TRANSACCIÓN
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // 1. VALIDACIONES
            var validationResult = await ValidatePayrollPeriod(command, cancellationToken);
            if (validationResult.IsFailure)
                return Result.Failure<long>(validationResult.Error);

            // 2. VALIDAR INTEGRIDAD DE DATOS
            /*var dataIntegrityResult = await ValidateDataIntegrity(command, cancellationToken);
            if (dataIntegrityResult.IsFailure)
                return Result.Failure<long>(dataIntegrityResult.Error);*/

            //return Result.Success<long>(0); // Temporalmente para evitar la ejecución completa


            // 3. CREAR PERÍODO DE PLANILLA
            var payrollPeriod = await CreatePayrollPeriod(command, cancellationToken);
            _logger.LogInformation("📅 Período de planilla creado: {PeriodId}", payrollPeriod.Id);

            // 4. OBTENER CONCEPTOS ACTIVOS
            var activeConcepts = await GetActiveConceptsByPayrollType(command.PayrollTypeId ?? 1, cancellationToken);
            _logger.LogInformation("📋 Conceptos activos cargados: {ConceptsCount}", activeConcepts.Count);

            // 5. OBTENER EMPLEADOS ACTIVOS
            var activeEmployees = await GetActiveEmployees(cancellationToken);
            _logger.LogInformation("👥 Empleados activos encontrados: {EmployeesCount}", activeEmployees.Count);

            // 6. PROCESAR PLANILLA POR CADA EMPLEADO
            decimal totalPlanillaAmount = 0;
            int processedEmployees = 0;
            var processedDetails = new List<PayrollDetail>();

            foreach (var employee in activeEmployees)
            {
                var employeePayroll = await ProcessEmployeePayroll(
                    payrollPeriod.Id,
                    employee,
                    command.StartDate,
                    command.EndDate,
                    activeConcepts,
                    cancellationToken);

                if (employeePayroll != null)
                {
                    processedDetails.Add(employeePayroll);
                    totalPlanillaAmount += employeePayroll.NetPay ?? 0;
                    processedEmployees++;

                    _logger.LogInformation("✅ Planilla procesada para {EmployeeCode}: Neto S/ {NetPay}",
                        employee.EmployeeCode, employeePayroll.NetPay);
                }
            }

            // 7. ACTUALIZAR TOTALES DEL PERÍODO
            await UpdatePayrollPeriodTotals(payrollPeriod.Id, totalPlanillaAmount, processedEmployees, cancellationToken);

            // 8. COMMIT
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("🎉 Planilla creada exitosamente. Período: {PeriodId}, Total: S/ {TotalAmount}, Empleados: {TotalEmployees}",
                payrollPeriod.Id, totalPlanillaAmount, processedEmployees);

            return Result.Success(payrollPeriod.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "❌ Error al crear planilla para período {PeriodName}", command.PeriodName);
            return Result.Failure<long>(PayrollPeriodErrores.ErrorSave);
        }
    }

    private async Task<Result> ValidateDataIntegrity(CreatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
        // Validar que exista el tipo de planilla
        var payrollTypeExists = await _payrollTypeRepository.ExistsAsync(
            pt => pt.Id == command.PayrollTypeId,
            cancellationToken);

        if (!payrollTypeExists)
        {
            _logger.LogWarning("⚠️ Tipo de planilla no encontrado: {PayrollTypeId}", command.PayrollTypeId);
            return Result.Failure(PayrollPeriodErrores.TipoPlanillaNoEncontrado);
        }

        // Validar que existan conceptos para este tipo de planilla
        var conceptsCount = await _conceptRepository.CountAsync(
            pc => pc.PayrollTypeId == command.PayrollTypeId && pc.StatePayrollConcept == 1,
            cancellationToken);

        if (conceptsCount == 0)
        {
            _logger.LogWarning("⚠️ No hay conceptos configurados para el tipo de planilla: {PayrollTypeId}", command.PayrollTypeId);
            return Result.Failure(PayrollPeriodErrores.ConceptosNoConfigurados);
        }


        // Validar parámetros legales esenciales
        var essentialParams = new[] { "UIT_2025", "TOPE_AFP_QUINCENAL_2025", "HORAS_MENSUALES", "RMV_2025" };
        foreach (var param in essentialParams)
        {
            var paramValue = await _legalParameters.GetCurrentParameterValue(param);
            if (paramValue == 0)
            {
                _logger.LogWarning("⚠️ Parámetro legal no configurado: {Parametro}", param);
                return Result.Failure(PayrollPeriodErrores.ParametroLegalNoConfigurado);
            }
        }
        _logger.LogDebug("✅ Integridad de datos validada correctamente");

        return Result.Success();
    }

    private async Task<Result> ValidatePayrollPeriod(CreatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
        // Validar período duplicado
        bool existsPeriod = await _payrollPeriodRepository.ExistsAsync(
            p => p.PeriodName == command.PeriodName && p.PayrollTypeId == command.PayrollTypeId 
            && p.StatePayrollPeriod == (int)EstadosEnum.Activo,
            cancellationToken);

        if (existsPeriod)
        {
            _logger.LogWarning("⚠️ Ya existe un período con el nombre: {PeriodName}", command.PeriodName);
            return Result.Failure(PayrollPeriodErrores.Duplicado);
        }

        // Validar fechas
        if (command.StartDate >= command.EndDate)
        {
            _logger.LogWarning("⚠️ Fechas inválidas: StartDate {StartDate} >= EndDate {EndDate}",
                command.StartDate, command.EndDate);
            return Result.Failure(PayrollPeriodErrores.FechasInvalidas);
        }

        // Validar que no exista período solapado
        bool overlappingPeriod = await _payrollPeriodRepository.ExistsAsync(
            p => p.StatePayrollPeriod == (int)EstadosEnum.Activo &&
                 ((p.StartDate <= command.StartDate && p.EndDate >= command.StartDate) ||
                  (p.StartDate <= command.EndDate && p.EndDate >= command.EndDate)),
            cancellationToken);

        if (overlappingPeriod)
        {
            _logger.LogWarning("⚠️ Período se solapa con otro período existente");
            return Result.Failure(PayrollPeriodErrores.PeriodoSolapado);
        }

        _logger.LogDebug("✅ Validaciones de período exitosas");
        return Result.Success();
    }

    private async Task<PayrollPeriod> CreatePayrollPeriod(CreatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
        var statusId = await GetStatusId("BORRADOR", cancellationToken);

        var payrollPeriod = PayrollPeriod.Create(
            command.PeriodName,
            command.PayrollTypeId,
            command.StartDate,
            command.EndDate,
            0, // TotalAmount se actualizará después
            0, // TotalEmployees se actualizará después
            statusId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _payrollPeriodRepository.AddAsync(payrollPeriod, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogDebug("📄 Período creado con ID: {PeriodId}", payrollPeriod.Id);
        return payrollPeriod;
    }



    private async Task<List<PayrollConcept>> GetActiveConceptsByPayrollType(long payrollTypeId, CancellationToken cancellationToken)
    {
        // Filtrar conceptos por tipo de planilla y estado activo, incluyendo las fórmulas
        var concepts = await _conceptRepository.ListAsync(
            pc => pc.StatePayrollConcept == (int)EstadosEnum.Activo
               && pc.PayrollTypeId == payrollTypeId,
            new[] { "PayrollFormula" }, // Incluir la fórmula relacionada
            cancellationToken);

        _logger.LogDebug("📋 {ConceptsCount} conceptos activos encontrados para tipo planilla {PayrollTypeId}", concepts.Count, payrollTypeId);

        // Log para verificar qué conceptos tienen fórmulas
        foreach (var concept in concepts)
        {
            if (concept.PayrollFormula != null)
            {
                _logger.LogDebug("📝 Concepto {ConceptCode} tiene fórmula: {FormulaName} ({FormulaExpression})",
                    concept.Code, concept.PayrollFormula.Name, concept.PayrollFormula.FormulaExpression);
            }
            else
            {
                _logger.LogDebug("📝 Concepto {ConceptCode} no tiene fórmula (Tipo cálculo: {CalculationType})",
                    concept.Code, concept.ConceptCalculationTypeId);
            }
        }

        return concepts.ToList();
    }

    private async Task<List<EmployeeInfo>> GetActiveEmployees(CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.ListAsync(
            e => e.StateEmployeeInfo == (int)EstadosEnum.Activo
            && e.Id == 2, // Temporalmente solo el empleado con Id 2
            cancellationToken);

        _logger.LogDebug("👥 {EmployeesCount} empleados activos encontrados", employees.Count);
        return employees.ToList();
    }

    private async Task<PayrollDetail> ProcessEmployeePayroll(
      long payrollPeriodId,
      EmployeeInfo employee,
      DateOnly? startDate,
      DateOnly? endDate,
      List<PayrollConcept> concepts,
      CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("🔍 Procesando planilla para empleado: {EmployeeCode} ({EmployeeId})",
                employee.EmployeeCode, employee.Id);

            // 1. OBTENER ASISTENCIA DEL EMPLEADO
            var attendanceRecords = await GetEmployeeAttendance(employee.Id, startDate, endDate, cancellationToken);

            if (!attendanceRecords.Any())
            {
                _logger.LogWarning("⚠️ Empleado {EmployeeCode} no tiene registros de asistencia en el período",
                    employee.EmployeeCode);
                return await CreateEmptyPayrollDetail(payrollPeriodId, employee, cancellationToken);
            }

            // OBTENER EL AÑO DEL PERÍODO
            var year = startDate?.Year ?? DateTime.Now.Year;

            // 2. CALCULAR INGRESOS Y DESCUENTOS TOTALES
            var totalIncome = await _payrollCalculationService.CalculateTotalIncome(
                employee, attendanceRecords, year, cancellationToken);
            var totalDeductions = await _payrollCalculationService.CalculateTotalDeductions(
                employee, attendanceRecords, year, cancellationToken);
            var netPay = totalIncome - totalDeductions;

            _logger.LogDebug("💰 Cálculos para {EmployeeCode}: Ingresos: S/ {Income}, Descuentos: S/ {Deductions}, Neto: S/ {NetPay}",
                employee.EmployeeCode, totalIncome, totalDeductions, netPay);

            // 3. CREAR PAYROLL DETAIL
            var payrollDetail = PayrollDetail.Create(
                payrollPeriodId,
                employee.Id,
                employee.CostCenterId,
                employee.BaseSalary / 2, // Salario base quincenal
                totalIncome,
                totalDeductions,
                netPay,
                await GetStatusId("CALCULADO", cancellationToken),
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                "system"
            );

            await _payrollDetailRepository.AddAsync(payrollDetail, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. CALCULAR Y CREAR DETALLES DE CONCEPTOS - PASAR EL AÑO
            await CalculateAndCreatePayrollDetailConcepts(
                payrollDetail.Id, concepts, employee, attendanceRecords, year, cancellationToken);

            _logger.LogInformation("✅ Planilla procesada para {EmployeeCode}: Neto S/ {NetPay}",
                employee.EmployeeCode, netPay);

            return payrollDetail;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al procesar planilla para empleado {EmployeeCode}", employee.EmployeeCode);
            return await CreateEmptyPayrollDetail(payrollPeriodId, employee, cancellationToken);
        }
    }

   private async Task<PayrollDetail> CreateEmptyPayrollDetail(long payrollPeriodId, EmployeeInfo employee, CancellationToken cancellationToken)
{
    // Obtener el año actual para consistencia
    var year = DateTime.Now.Year;
    
    // Crear un detalle vacío para empleados sin asistencia
    var payrollDetail = PayrollDetail.Create(
        payrollPeriodId,
        employee.Id,
        employee.CostCenterId,
        employee.BaseSalary / 2,
        0, // Sin ingresos
        0, // Sin descuentos
        0, // Neto cero
        await GetStatusId("BORRADOR", cancellationToken),
        (int)EstadosEnum.Activo,
        _dateTimeProvider.CurrentTime.ToUniversalTime(),
        "system"
    );

    await _payrollDetailRepository.AddAsync(payrollDetail, cancellationToken);
    
    _logger.LogDebug("📄 Detalle vacío creado para empleado {EmployeeCode} año {Year}", 
        employee.EmployeeCode, year);
        
    return payrollDetail;
}

    private async Task CalculateAndCreatePayrollDetailConcepts(
     long payrollDetailId,
     List<PayrollConcept> concepts,
     EmployeeInfo employee,
     List<AttendanceRecord> attendance,
     int year, // AGREGAR ESTE PARÁMETRO
     CancellationToken cancellationToken)
    {
        var createdConcepts = new List<PayrollDetailConcept>();

        foreach (var concept in concepts)
        {
            try
            {
                // PASAR EL PARÁMETRO YEAR AL SERVICIO
                var calculatedValue = await _payrollCalculationService.CalculateConceptValue(
                    concept, employee, attendance, year, cancellationToken);

                if (calculatedValue > 0)
                {
                    var detailConcept = PayrollDetailConcept.Create(
                        payrollDetailId,
                        concept.Id,
                        calculatedValue,
                        1, // quantity
                        calculatedValue,
                        $"{concept.Name} - Cálculo automático",
                        (int)EstadosEnum.Activo,
                        _dateTimeProvider.CurrentTime.ToUniversalTime(),
                        "system"
                    );

                    await _payrollDetailConceptRepository.AddAsync(detailConcept, cancellationToken);
                    createdConcepts.Add(detailConcept);

                    _logger.LogDebug("📝 Concepto {ConceptCode} calculado: S/ {Value} para empleado {EmployeeCode} año {Year}",
                        concept.Code, calculatedValue, employee.EmployeeCode, year);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error al calcular concepto {ConceptCode} para empleado {EmployeeCode} año {Year}",
                    concept.Code, employee.EmployeeCode, year);
            }
        }

        _logger.LogDebug("📊 {ConceptsCount} conceptos creados para empleado {EmployeeCode} año {Year}",
            createdConcepts.Count, employee.EmployeeCode, year);
    }
    private async Task<List<AttendanceRecord>> GetEmployeeAttendance(
        long employeeId,
        DateOnly? startDate,
        DateOnly? endDate,
        CancellationToken cancellationToken)
    {
        var attendance = await _attendanceRepository.ListAsync(
            a => a.EmployeeId == employeeId &&
                 a.RecordDate >= startDate &&
                 a.RecordDate <= endDate &&
                 a.StateAttendanceRecord == (int)EstadosEnum.Activo,
            cancellationToken);

        _logger.LogDebug("📅 {AttendanceRecords} registros de asistencia para empleado {EmployeeId}",
            attendance.Count, employeeId);

        return attendance.ToList();
    }

    private async Task UpdatePayrollPeriodTotals(long payrollPeriodId, decimal totalAmount, int totalEmployees, CancellationToken cancellationToken)
    {
        var payrollPeriod = await _payrollPeriodRepository.GetByIdAsync(payrollPeriodId, cancellationToken);
        if (payrollPeriod != null)
        {
            payrollPeriod.Update(
                payrollPeriodId,
                payrollPeriod.PeriodName,
                payrollPeriod.PayrollTypeId,
                payrollPeriod.StartDate,
                payrollPeriod.EndDate,
                totalAmount,
                totalEmployees,
                await GetStatusId("CALCULADO", cancellationToken),
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                "system"
            );

            _logger.LogInformation("📊 Período actualizado: Total S/ {TotalAmount}, Empleados: {TotalEmployees}",
                totalAmount, totalEmployees);
        }
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
}