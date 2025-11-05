using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Background;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Masters.Contadores;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Infrastructure.Services;

public class PayrollBackgroundProcessor(
    IGenericRepository<PayrollPeriod> _payrollPeriodRepository,
    IGenericRepository<PayrollDetail> _payrollDetailRepository,
    IGenericRepository<PayrollDetailConcept> _payrollDetailConceptRepository,
    IGenericRepository<PayrollPaymentRecord> _payrollPaymentRecordRepository,
    IGenericRepository<AccountingEntry> _accountingEntryRepository,
    IGenericRepository<AccountingEntryItem> _entryItemRepository,
    IGenericRepository<AccountingChart> _accountingChartRepository,
    IGenericRepository<Contador> _contadorRepository,
    IGenericRepository<EmployeeInfo> _employeeRepository,
    IGenericRepository<AttendanceRecord> _attendanceRepository,
    IGenericRepository<PayrollConceptType> _conceptTypeRepository,
    IPayrollCalculationService _payrollCalculationService,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<PayrollBackgroundProcessor> _logger
) : IPayrollBackgroundProcessor
{

    public async Task ProcessPayrollAsync(long payrollPeriodId, CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken)
    {
        _logger.LogInformation("🔄 Procesando planilla {PayrollPeriodId} en background", payrollPeriodId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. CARGAR DATOS DEL PERÍODO
            var payrollPeriod = await _payrollPeriodRepository.GetByIdAsync(payrollPeriodId, cancellationToken);
            if (payrollPeriod == null)
            {
                throw new InvalidOperationException($"Período de planilla {payrollPeriodId} no encontrado");
            }

            // 2. PROCESAR DETALLES Y CONCEPTOS
            await ProcessPayrollDetailsAsync(payrollPeriod, command,  periodName, cancellationToken);

            // Guardar los cambios antes de crear registros de pago para asegurar que NetPay esté actualizado
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 3. CREAR REGISTROS DE PAGO
            await CreatePaymentRecordsAsync(payrollPeriod, command, cancellationToken);

            // 4. GENERAR ASIENTO CONTABLE
            var accountingEntry = await GenerateAccountingEntryForPayroll(payrollPeriod, command, cancellationToken);

            // 5. ACTUALIZAR PERÍODO CON ESTADO COMPLETADO
            payrollPeriod.MarkAsCompleted();
            await _payrollPeriodRepository.UpdateAsync(payrollPeriod);

            // 6. GUARDAR TODO
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("✅ Planilla {PayrollPeriodId} procesada exitosamente en background", payrollPeriodId);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "❌ Error en procesamiento background para planilla {PayrollPeriodId}", payrollPeriodId);
            throw;
        }
    }

    private async Task ProcessPayrollDetailsAsync(PayrollPeriod payrollPeriod, CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken)
    {
        _logger.LogInformation("📊 Procesando detalles de planilla para período {PayrollPeriodId}", payrollPeriod.Id);

        // Obtener detalles pendientes
        var payrollDetails = await _payrollDetailRepository.ListAsync(
            pd => pd.PayrollPeriodId == payrollPeriod.Id &&
                  pd.StatePayrollDetail == (int)EstadosEnum.Activo,
            cancellationToken);

        // Obtener conceptos activos para el tipo de planilla
        var conceptTypes = await _conceptTypeRepository.ListAsync(
            pct => pct.PayrollTypeId == payrollPeriod.PayrollTypeId,
            new[] { "PayrollConcept.PayrollFormula" },
            cancellationToken);

        // Extraer solo los conceptos y filtrar los activos
        var concepts = conceptTypes
            .Where(pct => pct.PayrollConcept != null &&
                          pct.PayrollConcept.StatePayrollConcept == (int)EstadosEnum.Activo)
            .Select(pct => pct.PayrollConcept)
            .ToList();

        /*Console.WriteLine("=====>>> Conceptos cargados para el procesamiento:");
        foreach (var item in concepts)
        {
            Console.WriteLine($"Concepto cargado: {item!.Code} - {item.Name}");
            if (item.PayrollFormula != null)
            {
                 Console.WriteLine("¡Atención! SI tiene fórmula de nómina.");
                Console.WriteLine($"Descripción: {item.PayrollFormula!.FormulaExpression}");
            }else{
                Console.WriteLine("¡Atención! no tiene fórmula de nómina.");
            }
            
        }*/

        /*var concepts = await _conceptRepository.ListAsync(
            pc => pc.StatePayrollConcept == (int)EstadosEnum.Activo 
            && pc.PayrollTypeId == payrollPeriod.PayrollTypeId,
            new[] { "PayrollFormula" },
            cancellationToken);*/


        decimal totalPlanilla = 0;
        int empleadosProcesados = 0;
        foreach (var detail in payrollDetails)
        {
            // Obtener empleado y asistencia
            //var employee = await _employeeRepository.GetByIdAsync(detail.EmployeeId, cancellationToken);
            var specParams = new BaseSpecParams { Id = detail.EmployeeId };
            var spec = new EmployeeInfoSpecification(specParams);
            var employee = await _employeeRepository.GetEntityWithSpec(spec, cancellationToken);

            if (employee == null)
            {
                _logger.LogWarning("Empleado {EmployeeId} no encontrado para detalle {DetailId}", detail.EmployeeId, detail.Id);
                continue;
            }
            var attendance = await _attendanceRepository.ListAsync(
                a => a.EmployeeId == detail.EmployeeId &&
                     a.RecordDate >= payrollPeriod.StartDate &&
                     a.RecordDate <= payrollPeriod.EndDate &&
                     a.StateAttendanceRecord == (int)EstadosEnum.Activo,
                cancellationToken);

            var year = payrollPeriod.StartDate?.Year ?? DateTime.Now.Year;

            decimal totalIncome = 0;
            decimal totalDeductions = 0;
            decimal netPay = 0;

            // Calcular y crear conceptos detallados (TODOS los tipos)
            var createdConcepts = new List<PayrollDetailConcept>();
            foreach (var concept in concepts.ToList())
            {
                try
                {
                    var isFamilyAllowance = employee.IsFamilyAllowance ?? false;
                    if (concept!.Code == "ASIG_FAM" && !isFamilyAllowance)
                    {
                        _logger.LogInformation("Omitiendo concepto {ConceptCode} para empleado {EmployeeId} sin asignación familiar", concept.Code, detail.EmployeeId);
                        continue; // Omitir este concepto si el empleado no tiene asignación familiar
                    }
                    var value = await _payrollCalculationService.CalculateConceptValue(concept!, employee, attendance.ToList(), year, payrollPeriod.PayrollTypeId, cancellationToken);
                    if (value > 0)
                    {
                        var isDeduction = concept!.ConceptTypePayrollId == 2; // 1=Ingreso, 2=Descuento
                        if (isDeduction)
                            totalDeductions += value;
                        else
                            totalIncome += value;

                        var detailConcept = PayrollDetailConcept.Create(
                            detail.Id,
                            concept.Id,
                            value,
                            1,
                            value,
                            $"{concept.Name} - {periodName}",
                            (int)EstadosEnum.Activo,
                            _dateTimeProvider.CurrentTime.ToUniversalTime(),
                            command.CreatedBy
                        );
                        await _payrollDetailConceptRepository.AddAsync(detailConcept, cancellationToken);
                        createdConcepts.Add(detailConcept);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al calcular concepto {ConceptCode} para empleado {EmployeeId}", concept!.Code, detail.EmployeeId);
                }
            }

            // Guardar los conceptos creados físicamente antes de actualizar el detalle
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            netPay = totalIncome - totalDeductions;
            _logger.LogInformation("Totales para detalle {DetailId}: Ingresos={TotalIncome}, Deducciones={TotalDeductions}, Neto={NetPay}", detail.Id, totalIncome, totalDeductions, netPay);
            detail.UpdateTotals(totalIncome, totalDeductions, netPay);
            detail.MarkAsProcessed();
            await _payrollDetailRepository.UpdateAsync(detail);

            totalPlanilla += netPay;
            empleadosProcesados++;
        }

        // Actualizar totales en PayrollPeriod
        payrollPeriod.Update(
            payrollPeriod.Id,
            payrollPeriod.PeriodName,
            payrollPeriod.PayrollTypeId,
            payrollPeriod.StartDate,
            payrollPeriod.EndDate,
            totalPlanilla,
            empleadosProcesados,
            payrollPeriod.StatusId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _payrollPeriodRepository.UpdateAsync(payrollPeriod);

        _logger.LogInformation("✅ {DetailsCount} detalles de planilla procesados", payrollDetails.Count);
    }

    // Eliminados métodos simulados de cálculo y creación de conceptos. Todo se hace en ProcessPayrollDetailsAsync

    private async Task CreatePaymentRecordsAsync(PayrollPeriod payrollPeriod, CreatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("💰 Creando registros de pago para período {PayrollPeriodId}", payrollPeriod.Id);

        var payrollDetails = await _payrollDetailRepository.ListAsync(
            pd => pd.PayrollPeriodId == payrollPeriod.Id &&
                  pd.StatePayrollDetail == (int)EstadosEnum.Activo &&
                  pd.NetPay > 0,
            cancellationToken);

        int pagosCreados = 0;
        foreach (var detail in payrollDetails)
        {
            var paymentRecord = PayrollPaymentRecord.Create(
                detail.Id,
                DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.ToUniversalTime()),
                (long)PaymentMethodEnum.TransferenciaBancaria,
                detail.NetPay ?? 0,
                (long)CurrencyEnum.SolPeruano,
                1.00M,// Tipo de cambio fijo para PEN
                "TRANSFERENCIA " + detail.Employee!.EmployeeCode +" Cuenta "+ detail.Employee!.BankAccountNumber ,// CompanyBankAccountId
                                                               // Reference - usar ID de empleado como referencia
                null, // BankId
                null, // CompanyBankAccountId
                1, // PaymentStatusId - PENDIENTE
                null, // PaymentFilePath
                null, // ConfirmationDocumentPath                
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.CreatedBy
            );

            await _payrollPaymentRecordRepository.AddAsync(paymentRecord, cancellationToken);
            pagosCreados++;
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("✅ {PaymentRecordsCount} registros de pago creados", pagosCreados);
    }

    private async Task<AccountingEntry> GenerateAccountingEntryForPayroll(
        PayrollPeriod payrollPeriod,
        CreatePayrollPeriodCommand command,
        CancellationToken cancellationToken)
    {
        var entryNumber = await GenerateUniqueEntryNumber(command.CreatedBy!, cancellationToken);

        // Calcular totales
        var payrollDetails = await _payrollDetailRepository.ListAsync(
            pd => pd.PayrollPeriodId == payrollPeriod.Id &&
                  pd.StatePayrollDetail == (int)EstadosEnum.Activo,
            cancellationToken);

        var totalNetPay = payrollDetails.Sum(pd => pd.NetPay ?? 0);
        var totalDeductions = payrollDetails.Sum(pd => pd.TotalDeductions ?? 0);
        var totalIncome = payrollDetails.Sum(pd => pd.TotalIncome ?? 0);

        var accountingEntry = AccountingEntry.Create(
            entryNumber,
            payrollPeriod.StartDate ?? DateOnly.FromDateTime(DateTime.Now),
            $"Asiento de nómina - {payrollPeriod.PeriodName}",
            (long)SourceModuleEnum.Planillas,
            payrollPeriod.Id,
            totalNetPay + totalDeductions, // Total Débito
            totalNetPay + totalDeductions, // Total Crédito
            false,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _accountingEntryRepository.AddAsync(accountingEntry, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Generar items del asiento contable
        await GenerateAccountingEntryItemsForPayroll(accountingEntry, payrollPeriod, totalNetPay, totalDeductions, command, cancellationToken);

        return accountingEntry;
    }

    private async Task GenerateAccountingEntryItemsForPayroll(
        AccountingEntry accountingEntry,
        PayrollPeriod payrollPeriod,
        decimal totalNetPay,
        decimal totalDeductions,
        CreatePayrollPeriodCommand command,
        CancellationToken cancellationToken)
    {
        // 1. DÉBITO: Gastos de personal
        var debitAccountId = await GetAccountIdByCode("5.3.1.1", cancellationToken); // Gastos de sueldos y salarios


        var debitItem = AccountingEntryItem.Create(
            accountingEntry.Id,
            debitAccountId,
            totalNetPay + totalDeductions,
            0.00M,
            $"Gastos de nómina - {payrollPeriod.PeriodName}",
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _entryItemRepository.AddAsync(debitItem, cancellationToken);

        // 2. CRÉDITO: Sueldos por pagar (neto)
        var creditNetPayAccountId = await GetAccountIdByCode("5.3.1.2", cancellationToken); // Sueldos por pagar

        var creditNetPayItem = AccountingEntryItem.Create(
            accountingEntry.Id,
            creditNetPayAccountId,
            0.00M,
            totalNetPay,
            $"Sueldos por pagar - {payrollPeriod.PeriodName}",
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _entryItemRepository.AddAsync(creditNetPayItem, cancellationToken);

        // 3. CRÉDITO: Provisiones de ley (deducciones)
        var creditDeductionsAccountId = await GetAccountIdByCode("5.3.2.2", cancellationToken); // Provisiones

        var creditDeductionsItem = AccountingEntryItem.Create(
            accountingEntry.Id,
            creditDeductionsAccountId,
            0.00M,
            totalDeductions,
            $"Provisiones de ley - {payrollPeriod.PeriodName}",
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _entryItemRepository.AddAsync(creditDeductionsItem, cancellationToken);

        _logger.LogInformation("Items contables generados para planilla {PayrollPeriodId}: Débito {DebitAmount}, Créditos Neto {NetPay}, Provisiones {Deductions}",
            payrollPeriod.Id, totalNetPay + totalDeductions, totalNetPay, totalDeductions);
    }

    private async Task<string> GenerateUniqueEntryNumber(string createdBy, CancellationToken cancellationToken)
    {
        try
        {
            var today = DateTime.Today;
            var formattedDate = today.ToString("yyyyMMdd");

            var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("Planilla"), cancellationToken);

            if (contador == null)
            {
                var contadorNew = Contador.Create(
                    "Planilla",
                    "AP",
                    1,
                    string.Empty,
                    "string",
                    10,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    createdBy
                );

                await _contadorRepository.AddAsync(contadorNew, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                contador = contadorNew;
            }

            var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime(), createdBy, formattedDate);
            return nuevoCodigo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar número de asiento único para planilla");
            return $"AP-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }

    private async Task<long> GetAccountIdByCode(string accountCode, CancellationToken cancellationToken)
    {
        try
        {
            var accounts = await _accountingChartRepository.ListAsync(cancellationToken);
            var account = accounts.FirstOrDefault(a => a.AccountCode == accountCode);

            if (account == null)
            {
                _logger.LogWarning("No se encontró la cuenta contable con código {AccountCode}, usando cuenta por defecto", accountCode);
                var defaultAccount = accounts.FirstOrDefault(a => a.AccountCode == "1.1.1.1");
                return defaultAccount?.Id ?? 1;
            }

            return account.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar cuenta contable {AccountCode}", accountCode);
            return 1;
        }
    }
}
