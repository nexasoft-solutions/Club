using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriodPreview;

public class CreatePayrollPeriodPreviewCommandHandler(
    IGenericRepository<PayrollType> _payrollTypeRepository,
    IGenericRepository<EmployeeInfo> _employeeRepository,
    IGenericRepository<AttendanceRecord> _attendanceRepository,
    IGenericRepository<ConceptTypePayroll> _conceptTypePayrollRepository,
    IGenericRepository<PayrollConceptType> _conceptTypeRepository,
    IGenericRepository<PayrollConceptEmployeeType> _conceptEmployeeTypeRepository,
    IGenericRepository<PayrollConceptDepartment> _conceptDepartmentRepository,
    IGenericRepository<PayrollConceptEmployee> _conceptEmployeeRepository,
    IPayrollCalculationService _payrollCalculationService,
    ILogger<CreatePayrollPeriodPreviewCommandHandler> _logger
) : ICommandHandler<CreatePayrollPeriodPreviewCommand, PayrollPreviewResponse>
{
    public async Task<Result<PayrollPreviewResponse>> Handle(
        CreatePayrollPeriodPreviewCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("👀 Iniciando previsualización de planilla");

        try
        {
            // 1. OBTENER TIPO DE PLANILLA
            var payrollType = await _payrollTypeRepository.GetByIdAsync(command.PayrollTypeId!.Value, cancellationToken);
            if (payrollType == null)
                return Result.Failure<PayrollPreviewResponse>(PayrollPeriodErrores.TipoPlanillaNoEncontrado);

            // 2. GENERAR NOMBRE DEL PERÍODO
            var periodName = $"PLA-{payrollType.Code!.Trim()}-{command.StartDate:yyyyMM}";

            // 3. OBTENER EMPLEADOS ACTIVOS
            var activeEmployees = await GetActiveEmployees(cancellationToken);
            _logger.LogInformation("👥 {EmployeeCount} empleados activos encontrados", activeEmployees.Count);

            // 4. OBTENER CONCEPTOS ASOCIADOS AL TIPO DE PLANILLA

            var conceptTypes = await _conceptTypeRepository.ListAsync(
                pct => pct.PayrollTypeId == command.PayrollTypeId,
                new[] {
                    "PayrollConcept.PayrollFormula",
                    "PayrollConcept.AccountingChart"
                    },
                cancellationToken);

            // Extraer solo los conceptos y filtrar los activos
            var concepts = conceptTypes
                .Where(pct => pct.PayrollConcept != null &&
                              pct.PayrollConcept.StatePayrollConcept == (int)EstadosEnum.Activo)
                .Select(pct => pct.PayrollConcept)
                .ToList();

            _logger.LogInformation("📋 {ConceptCount} conceptos cargados para cálculo", concepts.Count);

            // 5. CALCULAR PREVISUALIZACIÓN POR EMPLEADO
            var employeePreviews = new List<EmployeePreviewRecord>();
            var conceptSummary = new Dictionary<string, ConceptSummaryRecord>();
            decimal totalPlanilla = 0;
            var year = command.StartDate.Year;

            foreach (var employee in activeEmployees)
            {
                var employeePreview = await CalculateEmployeePreview(
                    employee,
                    concepts.Where(c => c != null).Cast<PayrollConcept>().ToList(),
                    command,
                    year,
                    cancellationToken
                );

                employeePreviews.Add(employeePreview);
                totalPlanilla += employeePreview.NetPay;

                // ACTUALIZAR RESUMEN DE CONCEPTOS
                UpdateConceptSummary(conceptSummary, employeePreview.Concepts!);
            }

            // 6. CONSTRUIR RESPUESTA
            var response = new PayrollPreviewResponse(
                periodName,
                payrollType.Name ?? "N/A",
                command.StartDate,
                command.EndDate,
                employeePreviews.Count,
                totalPlanilla,
                employeePreviews,
                conceptSummary.Values.ToList()
            );


            _logger.LogInformation("✅ Previsualización completada: {Employees} empleados, Total: S/ {Total}",
                employeePreviews.Count, totalPlanilla);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error en previsualización de planilla");
            return Result.Failure<PayrollPreviewResponse>(PayrollPeriodErrores.ErrorPreview);
        }
    }

    private async Task<List<EmployeeInfoResponse>> GetActiveEmployees(CancellationToken cancellationToken)
    {
        BaseSpecParams SpecParams = new()
        {
            NoPaging = true,
            Sort = "employeeNameasc"
        };
        var spec = new EmployeeInfoSpecification(SpecParams);
        var employees = await _employeeRepository.ListAsync(spec, cancellationToken);
        return employees.ToList();
    }

    private async Task<EmployeePreviewRecord> CalculateEmployeePreview(
        EmployeeInfoResponse employee,
        List<PayrollConcept> concepts,
        CreatePayrollPeriodPreviewCommand command,
        int year,
        CancellationToken cancellationToken)
    {
        // OBTENER ASISTENCIA DEL EMPLEADO
        var attendance = await _attendanceRepository.ListAsync(
            a => a.EmployeeId == employee.Id &&
                 a.RecordDate >= command.StartDate &&
                 a.RecordDate <= command.EndDate &&
                 a.StateAttendanceRecord == (int)EstadosEnum.Activo,
            cancellationToken);

        var conceptDetails = new List<ConceptDetailRecord>();
        decimal totalIncome = 0;
        decimal totalDeductions = 0;


        // CALCULAR CADA CONCEPTO
        foreach (var concept in concepts)
        {
            if (await ConceptAppliesToEmployee(concept, employee, cancellationToken))
            {
                var isFamilyAllowance = employee.IsFamilyAllowance ?? false;
                if (concept!.Code == "ASIG_FAM" && !isFamilyAllowance)
                {
                    _logger.LogInformation("Omitiendo concepto {ConceptCode} para empleado {EmployeeId} sin asignación familiar", concept.Code, employee.Id);
                    continue; // Omitir este concepto si el empleado no tiene asignación familiar
                }
                var amount = await _payrollCalculationService.CalculateConceptValue(
                    concept, employee, attendance.ToList(), year, command.PayrollTypeId, cancellationToken);

                var conceptType = await _conceptTypePayrollRepository.GetByIdAsync(
                    concept.ConceptTypePayrollId!.Value, cancellationToken);

                if (amount > 0)
                {
                    var conceptDetail = new ConceptDetailRecord(
                        concept.Code ?? "N/A",
                        concept.Name ?? "N/A",
                        conceptType!.Code ?? "N/A",
                        amount,
                        concept.AccountingChart!.AccountCode ?? "N/A",
                        concept.AccountingChart!.AccountName ?? "N/A",
                        GetCalculationTypeDescription(concept.ConceptCalculationTypeId)
                    );

                    conceptDetails.Add(conceptDetail);

                    if (concept.ConceptTypePayrollId == 1)
                        totalIncome += amount;
                    else
                        totalDeductions += amount;
                }
            }
        }


        return new EmployeePreviewRecord
        (
            employee.Id,
            employee.EmployeeCode ?? "N/A",
            employee.FullName ?? "N/A",
            employee.Dni ?? "N/A",
            employee.DepartamentCode ?? "N/A",
            employee.BaseSalary,
            conceptDetails,
            totalIncome,
            totalDeductions,
            totalIncome - totalDeductions
        );
    }

    private decimal CalculateBasicSalary(EmployeeInfoResponse employee, long payrollTypeId)
    {
        // SUELDO BASE SEGÚN TIPO DE PLANILLA
        return payrollTypeId == (long)PayRollTypesEnum.Quincenal
            ? employee.BaseSalary / 2
            : employee.BaseSalary;
    }

    private async Task<bool> ConceptAppliesToEmployee(PayrollConcept concept, EmployeeInfoResponse employee, CancellationToken cancellationToken)
    {
        if (!concept.ConceptApplicationTypesId.HasValue)
            return true;

        switch (concept.ConceptApplicationTypesId.Value)
        {
            case 1: // TODOS
                return true;

            case 2: // POR TIPO DE EMPLEADO
                return await _conceptEmployeeTypeRepository.ExistsAsync(
                    cet => cet.PayrollConceptId == concept.Id &&
                           cet.EmployeeTypeId == employee.EmployeeTypeId &&
                           cet.StatePayrollConceptEmployeeType == 1,
                    cancellationToken);

            case 3: // POR DEPARTAMENTO
                return await _conceptDepartmentRepository.ExistsAsync(
                    cd => cd.PayrollConceptId == concept.Id &&
                          cd.DepartmentId == employee.DepartmentId &&
                          cd.StatePayrollConceptDepartment == 1,
                    cancellationToken);

            case 4: // INDIVIDUAL
                return await _conceptEmployeeRepository.ExistsAsync(
                    ce => ce.PayrollConceptId == concept.Id &&
                          ce.EmployeeId == employee.Id &&
                          ce.StatePayrollConceptEmployee == 1,
                    cancellationToken);

            default:
                return true;
        }
    }

    private string GetCalculationTypeDescription(long? calculationTypeId)
    {
        return calculationTypeId switch
        {
            1 => "FIJO",
            2 => "PORCENTAJE",
            3 => "FORMULA",
            4 => "VARIABLE",
            _ => "DESCONOCIDO"
        };
    }

    private string GetConceptTypePayrollDescription(long? conceptTypeId)
    {
        return conceptTypeId switch
        {
            1 => "FIJO",
            2 => "PORCENTAJE",
            3 => "FORMULA",
            4 => "VARIABLE",
            _ => "DESCONOCIDO"
        };
    }

    private void UpdateConceptSummary(Dictionary<string, ConceptSummaryRecord> summary, List<ConceptDetailRecord> concepts)
    {
        foreach (var concept in concepts)
        {
            if (summary.TryGetValue(concept.ConceptCode, out var existing))
            {
                summary[concept.ConceptCode] = existing with
                {
                    TotalAmount = existing.TotalAmount + concept.Amount,
                    AffectedEmployees = existing.AffectedEmployees + 1
                };
            }
            else
            {
                summary[concept.ConceptCode] = new ConceptSummaryRecord
                (
                    concept.ConceptCode,
                    concept.ConceptName,
                    concept.ConceptType,
                    concept.Amount,
                    1
                );
            }
        }
    }
}
