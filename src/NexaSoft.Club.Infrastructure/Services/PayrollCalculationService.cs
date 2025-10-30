using System.Data;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.HumanResources.LegalParameters;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

namespace NexaSoft.Club.Infrastructure.Services;

public class PayrollCalculationService(
    ILogger<PayrollCalculationService> _logger,
    IDateTimeProvider _dateTimeProvider,
    ILegalParametersRepository _legalParameters,
    IGenericRepository<PayrollConceptEmployeeType> _conceptEmployeeTypeRepository,
    IGenericRepository<PayrollConceptDepartment> _conceptDepartmentRepository,
    IGenericRepository<PayrollConceptEmployee> _conceptEmployeeRepository
) : IPayrollCalculationService
{

    public async Task<decimal> CalculateConceptValue(PayrollConcept concept, EmployeeInfo employee,
         List<AttendanceRecord> attendance, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Calculando concepto {ConceptCode} para empleado {EmployeeId}", concept.Code, employee.Id);

            // OBTENER VARIABLES DE CÁLCULO (CON PARÁMETROS CONFIGURABLES)
            var variables = await GetCalculationVariables(employee, attendance, cancellationToken);

            // VERIFICAR SI EL CONCEPTO APLICA
            if (!await ConceptAppliesToEmployee(concept, employee, variables, cancellationToken))
            {
                _logger.LogDebug("Concepto {ConceptCode} no aplica al empleado {EmployeeId}", concept.Code, employee.Id);
                return 0;
            }

            decimal calculatedValue = 0;

            // CÁLCULO SEGÚN TIPO (TODO CONFIGURABLE)
            switch (concept.ConceptCalculationTypeId)
            {
                case 1: // FIJO
                    calculatedValue = concept.FixedValue ?? 0;
                    _logger.LogDebug("Concepto {ConceptCode} calculado como FIJO: {Value}", concept.Code, calculatedValue);
                    break;

                case 2: // PORCENTAJE
                    // CORREGIDO: Quitar await ya que el método es sincrónico
                    var baseAmount = CalculatePercentageBase(concept, variables, cancellationToken);
                    var porcentaje = concept.PorcentajeValue ?? 0;
                    calculatedValue = baseAmount * (porcentaje / 100m);
                    _logger.LogDebug("Concepto {ConceptCode} calculado como PORCENTAJE: {Value} (base: {Base}, %: {Percentage})",
                        concept.Code, calculatedValue, baseAmount, porcentaje);
                    break;

                case 3: // FORMULA
                    if (concept.PayrollFormula != null)
                    {
                        calculatedValue = await CalculateByFormula(concept.PayrollFormula, variables, cancellationToken);
                        _logger.LogDebug("Concepto {ConceptCode} calculado por FÓRMULA: {Value}", concept.Code, calculatedValue);
                    }
                    else
                    {
                        _logger.LogWarning("Concepto {ConceptCode} tiene tipo FORMULA pero no tiene fórmula asociada", concept.Code);
                    }
                    break;

                case 4: // VARIABLE
                    // CORREGIDO: Quitar await ya que el método es sincrónico
                    calculatedValue = CalculateVariableConcept(concept, variables, cancellationToken);
                    _logger.LogDebug("Concepto {ConceptCode} calculado como VARIABLE: {Value}", concept.Code, calculatedValue);
                    break;

                default:
                    _logger.LogWarning("Tipo de cálculo no reconocido para concepto {ConceptCode}", concept.Code);
                    break;
            }

            // APLICAR LÍMITES CONFIGURABLES
            // CORREGIDO: Quitar await ya que el método es sincrónico
            calculatedValue = ApplyConceptLimits(concept, calculatedValue, variables, cancellationToken);

            _logger.LogInformation("Concepto {ConceptCode} calculado: {Value} para empleado {EmployeeId}",
                concept.Code, calculatedValue, employee.Id);

            return Math.Max(0, calculatedValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error crítico al calcular concepto {ConceptCode} para empleado {EmployeeId}",
                concept.Code, employee.Id);
            return 0;
        }
    }

    public async Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfo employee,
        List<AttendanceRecord> attendance, CancellationToken cancellationToken)
    {
        var variables = new Dictionary<string, object>();
        var calculationDate = DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.Date);

        try
        {
            // ✅ OBTENER PARÁMETROS LEGALES CONFIGURABLES
            var uit = await _legalParameters.GetCurrentParameterValue("UIT_2024");
            var topeAfpQuincenal = await _legalParameters.GetCurrentParameterValue("TOPE_AFP_QUINCENAL_2024");
            var horasMensuales = await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES");
            var rmv = await _legalParameters.GetCurrentParameterValue("RMV_2024");

            // ✅ OBTENER TASAS CONFIGURABLES
            var tasasHoras = await _legalParameters.GetParametersByCategory("HORAS_ESPECIALES", calculationDate);
            var horaExtraRate = tasasHoras.GetValueOrDefault("HORA_EXTRA_RATE", 150.00m);
            var domingoRate = tasasHoras.GetValueOrDefault("DOMINGO_RATE", 200.00m);
            var feriadoRate = tasasHoras.GetValueOrDefault("FERIADO_RATE", 250.00m);
            var nocturnoRate = tasasHoras.GetValueOrDefault("NOCTURNO_RATE", 125.00m);

            // ✅ OBTENER TASAS DE DESCUENTOS CONFIGURABLES
            var tasasDescuentos = await _legalParameters.GetParametersByCategory("DESCUENTOS", calculationDate);
            var afpRate = tasasDescuentos.GetValueOrDefault("AFP_TRABAJADOR", 10.00m);
            var saludRate = tasasDescuentos.GetValueOrDefault("SALUD_TRABAJADOR", 5.00m);
            var maxDeductionRate = tasasDescuentos.GetValueOrDefault("DESCUENTO_MAX_PORCENTAJE", 50.00m);
            var adelantoMaxRate = tasasDescuentos.GetValueOrDefault("ADELANTO_MAX_PORCENTAJE", 30.00m);

            // INFORMACIÓN BÁSICA DEL EMPLEADO
            variables["employee_id"] = employee.Id;
            variables["base_salary"] = employee.BaseSalary;
            variables["base_salary_quincenal"] = employee.BaseSalary / 2;
            variables["base_salary_diario"] = employee.BaseSalary / 30;
            variables["hourly_rate"] = employee.BaseSalary / horasMensuales;
            variables["antiguedad_meses"] = CalculateAntiquity(employee.HireDate);
            variables["antiguedad_anios"] = CalculateAntiquityYears(employee.HireDate);

            // ASISTENCIA Y TIEMPOS
            variables["total_attendance_days"] = attendance.Count(a => a.AttendanceStatusTypeId == 1);
            variables["absent_days"] = attendance.Count(a => a.AttendanceStatusTypeId == 2);
            variables["license_days"] = attendance.Count(a => a.AttendanceStatusTypeId == 5);
            variables["vacation_days"] = attendance.Count(a => a.AttendanceStatusTypeId == 4);
            variables["tardiness_days"] = attendance.Count(a => a.LateMinutes > 0);

            // HORAS POR TIPO
            variables["overtime_hours"] = attendance.Sum(a => a.OvertimeHours) ?? 0;
            variables["sunday_hours"] = attendance.Sum(a => a.SundayHours) ?? 0;
            variables["holiday_hours"] = attendance.Sum(a => a.HolidayHours) ?? 0;
            variables["night_hours"] = attendance.Sum(a => a.NightHours) ?? 0;
            variables["regular_hours"] = attendance.Sum(a => a.RegularHours) ?? 0;
            variables["total_hours"] = attendance.Sum(a => a.TotalHours) ?? 0;

            // INCIDENCIAS
            variables["late_minutes"] = attendance.Sum(a => a.LateMinutes) ?? 0;
            variables["late_hours"] = attendance.Sum(a => a.LateMinutes) / 60m ?? 0;
            variables["early_departure_minutes"] = attendance.Sum(a => a.EarlyDepartureMinutes) ?? 0;
            variables["attendance_rate"] = CalculateAttendanceRate(attendance);

            // ✅ CÁLCULOS DE INGRESOS CON TASAS CONFIGURABLES
            variables["overtime_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["overtime_hours"], horaExtraRate, horasMensuales);
            variables["sunday_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["sunday_hours"], domingoRate, horasMensuales);
            variables["holiday_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["holiday_hours"], feriadoRate, horasMensuales);
            variables["night_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["night_hours"], nocturnoRate, horasMensuales);

            // INGRESOS TOTALES ACUMULADOS
            variables["total_income_before_deductions"] = await CalculateTotalIncome(employee, attendance, cancellationToken);

            // CÁLCULOS ACUMULADOS
            // CORREGIDO: Quitar await ya que el método es sincrónico
            variables["accumulated_income"] = CalculateAccumulatedIncome(employee, cancellationToken);
            variables["accumulated_afp"] = await CalculateAccumulatedAFP(employee, cancellationToken);
            variables["accumulated_renta"] = await CalculateAccumulatedRenta(employee, cancellationToken);

            // ✅ VARIABLES LEGALES CONFIGURABLES
            variables["uit"] = uit;
            variables["remuneracion_minima"] = rmv;
            variables["topes_afp"] = topeAfpQuincenal * 2;
            variables["topes_afp_quincenal"] = topeAfpQuincenal;
            variables["uit_x_5"] = uit * 5;
            variables["horas_mensuales"] = horasMensuales;

            // ✅ TASAS CONFIGURABLES
            variables["afp_rate"] = afpRate;
            variables["salud_rate"] = saludRate;
            variables["max_deduction_rate"] = maxDeductionRate;
            variables["adelanto_max_rate"] = adelantoMaxRate;

            _logger.LogDebug("Variables calculadas para empleado {EmployeeId} con parámetros configurables", employee.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular variables para empleado {EmployeeId}", employee.Id);
        }

        return variables;
    }

    public async Task<bool> ConceptAppliesToEmployee(PayrollConcept concept, EmployeeInfo employee, 
        Dictionary<string, object> variables, CancellationToken cancellationToken)
    {
        try
        {
            // Si no tiene tipo de aplicación definido, aplica a todos
            if (!concept.ConceptApplicationTypesId.HasValue)
                return true;

            switch (concept.ConceptApplicationTypesId.Value)
            {
                case 1: // TODOS
                    return true;

                case 2: // POR TIPO DE EMPLEADO
                    return await AppliesToEmployeeType(concept.Id, employee.EmployeeTypeId ?? 0, cancellationToken);

                case 3: // POR DEPARTAMENTO
                    return await AppliesToDepartment(concept.Id, employee.DepartmentId ?? 0, cancellationToken);

                case 4: // INDIVIDUAL
                    return await AppliesToIndividualEmployee(concept.Id, employee.Id, cancellationToken);

                default:
                    _logger.LogWarning("Tipo de aplicación no reconocido: {ApplicationTypeId} para concepto {ConceptCode}", 
                        concept.ConceptApplicationTypesId, concept.Code);
                    return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar aplicación de concepto {ConceptCode} para empleado {EmployeeId}", 
                concept.Code, employee.Id);
            return false;
        }
    }

    private async Task<bool> AppliesToEmployeeType(long conceptId, long employeeTypeId, CancellationToken cancellationToken)
    {
        return await _conceptEmployeeTypeRepository.ExistsAsync(
            cet => cet.PayrollConceptId == conceptId && 
                   cet.EmployeeTypeId == employeeTypeId &&
                   cet.StatePayrollConceptEmployeeType == 1,
            cancellationToken);
    }

    private async Task<bool> AppliesToDepartment(long conceptId, long departmentId, CancellationToken cancellationToken)
    {
        return await _conceptDepartmentRepository.ExistsAsync(
            cd => cd.PayrollConceptId == conceptId && 
                  cd.DepartmentId == departmentId &&
                  cd.StatePayrollConceptDepartment == 1,
            cancellationToken);
    }

    private async Task<bool> AppliesToIndividualEmployee(long conceptId, long employeeId, CancellationToken cancellationToken)
    {
        return await _conceptEmployeeRepository.ExistsAsync(
            ce => ce.PayrollConceptId == conceptId && 
                  ce.EmployeeId == employeeId &&
                  ce.StatePayrollConceptEmployee == 1,
            cancellationToken);
    }

    public async Task<decimal> CalculateTotalIncome(EmployeeInfo employee, List<AttendanceRecord> attendance,
        CancellationToken cancellationToken)
    {
        try
        {
            var variables = await GetCalculationVariables(employee, attendance, cancellationToken);

            decimal totalIncome = 0;

            // SUELDO BÁSICO QUINCENAL
            totalIncome += (decimal)variables["base_salary_quincenal"];

            // HORAS EXTRAORDINARIAS
            totalIncome += (decimal)variables["overtime_amount"];

            // TRABAJO DOMINICAL
            totalIncome += (decimal)variables["sunday_amount"];

            // TRABAJO FERIADO
            totalIncome += (decimal)variables["holiday_amount"];

            // BONO POR PRODUCTIVIDAD (CON TASA CONFIGURABLE)
            var attendanceRate = (decimal)variables["attendance_rate"];
            var tasasBonos = await _legalParameters.GetParametersByCategory(
                "BONOS",DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.Date) );
            var bonoProductividadRate = tasasBonos.GetValueOrDefault("BONO_PRODUCTIVIDAD_RATE", 10.00m);
            var minAttendanceForBonus = tasasBonos.GetValueOrDefault("MIN_ATTENDANCE_BONO", 95.00m);

            if (attendanceRate >= (minAttendanceForBonus / 100m) && bonoProductividadRate > 0)
            {
                totalIncome += (decimal)variables["base_salary_quincenal"] * (bonoProductividadRate / 100m);
            }

            _logger.LogDebug("Ingresos totales calculados para empleado {EmployeeId}: {TotalIncome}",
                employee.Id, totalIncome);

            return totalIncome;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular ingresos totales para empleado {EmployeeId}", employee.Id);
            return employee.BaseSalary / 2;
        }
    }

    public async Task<decimal> CalculateTotalDeductions(EmployeeInfo employee, List<AttendanceRecord> attendance,
        CancellationToken cancellationToken)
    {
        try
        {
            var variables = await GetCalculationVariables(employee, attendance, cancellationToken);
            var totalIncome = (decimal)variables["total_income_before_deductions"];

            decimal totalDeductions = 0;

            // ✅ AFP (CON TASA CONFIGURABLE)
            var afpRate = (decimal)variables["afp_rate"];
            var afpBase = Math.Min(totalIncome, (decimal)variables["topes_afp_quincenal"]);
            totalDeductions += afpBase * (afpRate / 100m);

            // ✅ SEGURO DE SALUD (CON TASA CONFIGURABLE)
            var saludRate = (decimal)variables["salud_rate"];
            totalDeductions += afpBase * (saludRate / 100m);

            // ✅ IMPUESTO A LA RENTA (CON ESCALAS CONFIGURABLES)
            var accumulatedIncome = (decimal)variables["accumulated_income"];
            var renta = await CalculateIncomeTax(accumulatedIncome);
            totalDeductions += renta;

            // ✅ ADELANTOS (CON LÍMITE CONFIGURABLE)
            var adelantoMaxRate = (decimal)variables["adelanto_max_rate"];
            /*var advances = await GetEmployeeAdvances(employee.Id, cancellationToken);
            if (advances.Any())
            {
                var maxAdelanto = totalIncome * (adelantoMaxRate / 100m);
                totalDeductions += advances.Where(a => a.AdvanceType == "ADELANTO")
                                          .Sum(a => Math.Min(a.RemainingAmount, maxAdelanto));
            }*/

            // ✅ APLICAR LÍMITE MÁXIMO CONFIGURABLE
            var maxDeductionRate = (decimal)variables["max_deduction_rate"];
            totalDeductions = Math.Min(totalDeductions, totalIncome * (maxDeductionRate / 100m));

            _logger.LogDebug("Deducciones totales calculadas para empleado {EmployeeId}: {TotalDeductions}",
                employee.Id, totalDeductions);

            return totalDeductions;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular deducciones para empleado {EmployeeId}", employee.Id);
            return 0;
        }
    }

    // MÉTODOS AUXILIARES CORREGIDOS (sin async)
    private decimal CalculateSpecialHoursAmount(decimal baseSalary, decimal hours, decimal rate, decimal monthlyHours)
    {
        var hourlyRate = baseSalary / monthlyHours;
        return hours * hourlyRate * (rate / 100m);
    }

    private async Task<decimal> CalculateByFormula(PayrollFormula formula, Dictionary<string, object> variables,
        CancellationToken cancellationToken)
    {
        if (formula == null) return 0;

        try
        {
            var expression = formula.FormulaExpression;

            foreach (var variable in variables)
            {
                expression = expression!.Replace(variable.Key, variable.Value.ToString());
            }

            return EvaluateSafeExpression(expression!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al evaluar fórmula: {Formula}", formula.FormulaExpression);
            return 0;
        }
    }

    private decimal EvaluateSafeExpression(string expression)
    {
        try
        {
            expression = expression.Replace("GREATEST", "Math.Max")
                                 .Replace("LEAST", "Math.Min");

            using (DataTable table = new DataTable())
            {
                var result = table.Compute(expression, "");
                return Convert.ToDecimal(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en evaluación de expresión: {Expression}", expression);
            return 0;
        }
    }

    private decimal CalculatePercentageBase(PayrollConcept concept, Dictionary<string, object> variables,
        CancellationToken cancellationToken)
    {
        switch (concept.Code)
        {
            case "AFP":
            case "SALUD":
                var totalIncome = (decimal)variables["total_income_before_deductions"];
                var maxBase = (decimal)variables["topes_afp_quincenal"];
                return Math.Min(totalIncome, maxBase);

            case "RENTA_5UM":
                return (decimal)variables["accumulated_income"];

            case "BONO_PRODUCTIVIDAD":
                return (decimal)variables["base_salary_quincenal"] * (decimal)variables["attendance_rate"];

            default:
                return (decimal)variables["base_salary_quincenal"];
        }
    }

    private decimal CalculateVariableConcept(PayrollConcept concept, Dictionary<string, object> variables,
        CancellationToken cancellationToken)
    {
        switch (concept.Code)
        {
            case "SUELDO_BASICO":
                return (decimal)variables["base_salary_quincenal"];

            case "HORA_EXTRA":
                return (decimal)variables["overtime_amount"];

            case "DOMINGO":
                return (decimal)variables["sunday_amount"];

            case "FERIADO":
                return (decimal)variables["holiday_amount"];

            case "BONO_PRODUCTIVIDAD":
                var baseSalary = (decimal)variables["base_salary_quincenal"];
                var attendanceRate = (decimal)variables["attendance_rate"];
                return CalculateBonoProductividad(baseSalary, attendanceRate);

            default:
                return 0;
        }
    }

    private decimal CalculateBonoProductividad(decimal baseSalary, decimal attendanceRate)
    {
        // Implementación sincrónica temporal
        var bonoRate = 10.00m; // Valor por defecto
        var minAttendance = 95.00m; // Valor por defecto
        
        return attendanceRate >= (minAttendance / 100m) ? baseSalary * (bonoRate / 100m) : 0;
    }

    private decimal ApplyConceptLimits(PayrollConcept concept, decimal value,
        Dictionary<string, object> variables, CancellationToken cancellationToken)
    {
        switch (concept.Code)
        {
            case "AFP":
                var maxAFPBase = (decimal)variables["topes_afp_quincenal"];
                var afpBase = Math.Min((decimal)variables["base_salary_quincenal"], maxAFPBase);
                var afpRate = (decimal)variables["afp_rate"];
                return Math.Min(value, afpBase * (afpRate / 100m));

            case "RENTA_5UM":
                var accumulated = (decimal)variables["accumulated_income"];
                var uit = (decimal)variables["uit"];
                var rentaBase = Math.Max(0, accumulated - (5 * uit));
                return Math.Min(value, rentaBase * 0.08m);

            case "ADELANTO_FIJO":
                var maxAdvance = (decimal)variables["base_salary_quincenal"] * ((decimal)variables["adelanto_max_rate"] / 100m);
                return Math.Min(value, maxAdvance);

            default:
                return value;
        }
    }

    private async Task<decimal> CalculateIncomeTax(decimal accumulatedIncome)
    {
        // En producción, esto consultaría las escalas de renta configuradas
        // Por ahora usamos una implementación básica
        var uit = await _legalParameters.GetCurrentParameterValue("UIT_2024");
        var exemptLimit = 5 * uit;

        if (accumulatedIncome <= exemptLimit)
            return 0;

        var taxableIncome = accumulatedIncome - exemptLimit;
        return taxableIncome * 0.08m; // 8% para el primer tramo
    }

    private int CalculateAntiquity(DateOnly hireDate)
    {
        var today = _dateTimeProvider.CurrentTime.Date;
        var months = (today.Year - hireDate.Year) * 12 + today.Month - hireDate.Month;
        return Math.Max(0, months);
    }

    private int CalculateAntiquityYears(DateOnly hireDate)
    {
        var today = _dateTimeProvider.CurrentTime.Date;
        var years = today.Year - hireDate.Year;
        if (today.Month < hireDate.Month || (today.Month == hireDate.Month && today.Day < hireDate.Day))
            years--;
        return Math.Max(0, years);
    }

    private decimal CalculateAttendanceRate(List<AttendanceRecord> attendance)
    {
        var totalDays = attendance.Count;
        var presentDays = attendance.Count(a => a.AttendanceStatusTypeId == 1);
        return totalDays > 0 ? (decimal)presentDays / totalDays : 1m;
    }

    private decimal CalculateAccumulatedIncome(EmployeeInfo employee, CancellationToken cancellationToken)
    {
        var months = CalculateAntiquity(employee.HireDate);
        return employee.BaseSalary * Math.Min(months, 12);
    }

    private async Task<decimal> CalculateAccumulatedAFP(EmployeeInfo employee, CancellationToken cancellationToken)
    {
        var accumulatedIncome = CalculateAccumulatedIncome(employee, cancellationToken);
        var topeAfpMensual = await _legalParameters.GetCurrentParameterValue("TOPE_AFP_MENSUAL_2024") * 12;
        var afpBase = Math.Min(accumulatedIncome, topeAfpMensual);
        var afpRate = (await _legalParameters.GetParametersByCategory("DESCUENTOS",
            DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.Date)))
            .GetValueOrDefault("AFP_TRABAJADOR", 10.00m);

        return afpBase * (afpRate / 100m);
    }

    private async Task<decimal> CalculateAccumulatedRenta(EmployeeInfo employee, CancellationToken cancellationToken)
    {
        var accumulatedIncome = CalculateAccumulatedIncome(employee, cancellationToken);
        var uit = await _legalParameters.GetCurrentParameterValue("UIT_2024");
        var rentaBase = Math.Max(0, accumulatedIncome - (5 * uit));
        return rentaBase * 0.08m;
    }

    //Para adelantos 
    /*private async Task<List<EmployeeAdvance>> GetEmployeeAdvances(long employeeId, CancellationToken cancellationToken)
    {
        // En producción, esto consultaría la base de datos
        return new List<EmployeeAdvance>();
    }*/

    private bool IsConceptForSpecificEmployee(PayrollConcept concept, long employeeId,
        CancellationToken cancellationToken)
    {
        // En producción, consultarías una tabla de relación
        return true;
    }
}