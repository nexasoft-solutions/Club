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
         List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Calculando concepto {ConceptCode} para empleado {EmployeeId} año {Year}",
                concept.Code, employee.Id, year);

            // OBTENER VARIABLES DE CÁLCULO
            var variables = await GetCalculationVariables(employee, attendance, year, cancellationToken);

            // VERIFICAR SI EL CONCEPTO APLICA
            if (!await ConceptAppliesToEmployee(concept, employee, variables, cancellationToken))
            {
                _logger.LogDebug("Concepto {ConceptCode} no aplica al empleado {EmployeeId}", concept.Code, employee.Id);
                return 0;
            }

            decimal calculatedValue = 0;

            // CÁLCULO SEGÚN TIPO
            switch (concept.ConceptCalculationTypeId)
            {
                case 1: // FIJO
                    calculatedValue = concept.FixedValue ?? 0;
                    _logger.LogDebug("Concepto {ConceptCode} calculado como FIJO: {Value}", concept.Code, calculatedValue);
                    break;

                case 2: // PORCENTAJE
                    var baseAmount = CalculatePercentageBase(concept, variables);
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
                    calculatedValue = CalculateVariableConcept(concept, variables);
                    _logger.LogDebug("Concepto {ConceptCode} calculado como VARIABLE: {Value}", concept.Code, calculatedValue);
                    break;

                default:
                    _logger.LogWarning("Tipo de cálculo no reconocido para concepto {ConceptCode}", concept.Code);
                    break;
            }

            // APLICAR LÍMITES CONFIGURABLES
            calculatedValue = ApplyConceptLimits(concept, calculatedValue, variables);

            _logger.LogInformation("Concepto {ConceptCode} calculado: {Value} para empleado {EmployeeId}",
                concept.Code, calculatedValue, employee.Id);

            return Math.Max(0, calculatedValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error crítico al calcular concepto {ConceptCode} para empleado {EmployeeId} año {Year}",
                concept.Code, employee.Id, year);
            return 0;
        }
    }

    public async Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfo employee,
        List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken)
    {
        var variables = new Dictionary<string, object>();
        var calculationDate = DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.Date);

        try
        {
            // OBTENER PARÁMETROS LEGALES CONFIGURABLES
            var uit = await GetParameterByYear("UIT", year, 4950.00m);
            var topeAfpQuincenal = await GetParameterByYear("TOPE_AFP_QUINCENAL", year, 4477.50m);
            var horasMensuales = await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES");
            var rmv = await GetParameterByYear("RMV", year, 1025.00m);
            var topeAfpMensual = await GetParameterByYear("TOPE_AFP_MENSUAL", year, 8955.00m);

            // OBTENER TASAS CONFIGURABLES
            var tasasHoras = await _legalParameters.GetParametersByCategory("HORAS_ESPECIALES", calculationDate);
            var horaExtraRate = tasasHoras.GetValueOrDefault("HORA_EXTRA_RATE", 150.00m);
            var domingoRate = tasasHoras.GetValueOrDefault("DOMINGO_RATE", 200.00m);
            var feriadoRate = tasasHoras.GetValueOrDefault("FERIADO_RATE", 250.00m);
            var nocturnoRate = tasasHoras.GetValueOrDefault("NOCTURNO_RATE", 125.00m);

            // OBTENER TASAS DE DESCUENTOS CONFIGURABLES
            var tasasDescuentos = await _legalParameters.GetParametersByCategory("DESCUENTOS", calculationDate);
            var afpRate = tasasDescuentos.GetValueOrDefault("AFP_TRABAJADOR", 10.00m);
            var saludRate = tasasDescuentos.GetValueOrDefault("SALUD_TRABAJADOR", 5.00m);
            var maxDeductionRate = tasasDescuentos.GetValueOrDefault("DESCUENTO_MAX_PORCENTAJE", 50.00m);
            var adelantoMaxRate = tasasDescuentos.GetValueOrDefault("ADELANTO_MAX_PORCENTAJE", 30.00m);

            // ✅ VERIFICACIÓN CRÍTICA DEL SALARIO
            _logger.LogInformation("💰 SALARIO BASE VERIFICACIÓN - Empleado: {EmployeeCode}, Base Salary: {BaseSalary}",
                employee.EmployeeCode, employee.BaseSalary);

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

            // CÁLCULOS DE INGRESOS CON TASAS CONFIGURABLES
            variables["overtime_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["overtime_hours"], horaExtraRate, horasMensuales);
            variables["sunday_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["sunday_hours"], domingoRate, horasMensuales);
            variables["holiday_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["holiday_hours"], feriadoRate, horasMensuales);
            variables["night_amount"] = CalculateSpecialHoursAmount(
                employee.BaseSalary, (decimal)variables["night_hours"], nocturnoRate, horasMensuales);

            // ✅ CORREGIDO: total_income = Sueldo básico + Ingresos adicionales
            var ingresosAdicionales = CalculateIngresosAdicionales(employee, variables);
            variables["total_income"] = (decimal)variables["base_salary_quincenal"] + ingresosAdicionales;
            variables["total_income_before_deductions"] = variables["total_income"];

            // CÁLCULOS ACUMULADOS
            variables["accumulated_income"] = CalculateAccumulatedIncome(employee);
            variables["accumulated_afp"] = await CalculateAccumulatedAFP(employee, year, cancellationToken);
            variables["accumulated_renta"] = await CalculateAccumulatedRenta(employee, year, cancellationToken);

            // VARIABLES LEGALES CONFIGURABLES
            variables["uit"] = uit;
            variables["remuneracion_minima"] = rmv;
            variables["topes_afp"] = topeAfpMensual;
            variables["topes_afp_quincenal"] = topeAfpQuincenal;
            variables["uit_x_5"] = uit * 5;
            variables["horas_mensuales"] = horasMensuales;
            variables["year"] = year;

            // TASAS CONFIGURABLES
            variables["afp_rate"] = afpRate;
            variables["salud_rate"] = saludRate;
            variables["max_deduction_rate"] = maxDeductionRate;
            variables["adelanto_max_rate"] = adelantoMaxRate;

            // ✅ LOG DETALLADO DE CÁLCULOS
            _logger.LogInformation("💰 CÁLCULOS FINALES - Empleado: {EmployeeCode}", employee.EmployeeCode);
            _logger.LogInformation("   - Sueldo base: {BaseSalary}", employee.BaseSalary);
            _logger.LogInformation("   - Sueldo quincenal: {Quincenal}", variables["base_salary_quincenal"]);
            _logger.LogInformation("   - Ingresos adicionales: {Adicionales}", ingresosAdicionales);
            _logger.LogInformation("   - Total income: {TotalIncome}", variables["total_income"]);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular variables para empleado {EmployeeId} año {Year}", employee.Id, year);

            // Valores por defecto
            var defaultValues = GetDefaultParametersByYear(year);
            foreach (var kvp in defaultValues)
            {
                variables[kvp.Key] = kvp.Value;
            }

            // GARANTIZAR QUE total_income EXISTA INCLUSO EN ERROR
            if (!variables.ContainsKey("total_income"))
            {
                variables["total_income"] = employee.BaseSalary / 2;
            }
        }

        return variables;
    }

    

    // ✅ CORREGIDO: Solo calcular ingresos adicionales (NO incluir sueldo básico)
    private decimal CalculateIngresosAdicionales(EmployeeInfo employee, Dictionary<string, object> variables)
    {
        try
        {
            decimal ingresosAdicionales = 0;

            // SOLO INGRESOS ADICIONALES
            ingresosAdicionales += (decimal)variables["overtime_amount"];
            ingresosAdicionales += (decimal)variables["sunday_amount"];
            ingresosAdicionales += (decimal)variables["holiday_amount"];
            ingresosAdicionales += (decimal)variables["night_amount"];

            _logger.LogDebug("📊 Ingresos adicionales para {EmployeeCode}: S/ {AdditionalIncome}",
                employee.EmployeeCode, ingresosAdicionales);

            return ingresosAdicionales;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular ingresos adicionales para empleado {EmployeeCode}", employee.EmployeeCode);
            return 0;
        }
    }

    // ✅ MANTENER ESTE MÉTODO PARA COMPATIBILIDAD (pero ya no se usa para total_income)
    private decimal CalculateTotalIncomeForFormulas(EmployeeInfo employee, Dictionary<string, object> variables)
    {
        return CalculateIngresosAdicionales(employee, variables);
    }

    private async Task<decimal> GetParameterByYear(string parameterBase, int year, decimal defaultValue)
    {
        try
        {
            var parameterName = $"{parameterBase}_{year}";
            var value = await _legalParameters.GetCurrentParameterValue(parameterName);

            if (value > 0)
                return value;

            parameterName = $"{parameterBase}_{year - 1}";
            value = await _legalParameters.GetCurrentParameterValue(parameterName);

            if (value > 0)
                return value;

            _logger.LogWarning("Parámetro {ParameterBase} no encontrado para año {Year}, usando valor por defecto: {DefaultValue}",
                parameterBase, year, defaultValue);
            return defaultValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener parámetro {ParameterBase} para año {Year}", parameterBase, year);
            return defaultValue;
        }
    }

    private Dictionary<string, object> GetDefaultParametersByYear(int year)
    {
        var defaults = new Dictionary<string, object>();

        switch (year)
        {
            case 2024:
                defaults["uit"] = 4950.00m;
                defaults["remuneracion_minima"] = 1025.00m;
                defaults["topes_afp_quincenal"] = 4477.50m;
                defaults["topes_afp"] = 8955.00m;
                break;
            case 2025:
                defaults["uit"] = 5050.00m;
                defaults["remuneracion_minima"] = 1100.00m;
                defaults["topes_afp_quincenal"] = 4600.00m;
                defaults["topes_afp"] = 9200.00m;
                break;
            default:
                defaults["uit"] = 5000.00m;
                defaults["remuneracion_minima"] = 1050.00m;
                defaults["topes_afp_quincenal"] = 4500.00m;
                defaults["topes_afp"] = 9000.00m;
                break;
        }

        defaults["horas_mensuales"] = 240.00m;
        defaults["uit_x_5"] = (decimal)defaults["uit"] * 5;
        defaults["afp_rate"] = 10.00m;
        defaults["salud_rate"] = 5.00m;
        defaults["max_deduction_rate"] = 50.00m;
        defaults["adelanto_max_rate"] = 30.00m;
        defaults["year"] = year;
        defaults["total_income"] = 2500.00m;

        return defaults;
    }

    public async Task<decimal> CalculateTotalIncome(EmployeeInfo employee, List<AttendanceRecord> attendance,
        int year, CancellationToken cancellationToken)
    {
        var variables = await GetCalculationVariables(employee, attendance, year, cancellationToken);
        return (decimal)variables["total_income_before_deductions"];
    }

    public async Task<bool> ConceptAppliesToEmployee(PayrollConcept concept, EmployeeInfo employee,
        Dictionary<string, object> variables, CancellationToken cancellationToken)
    {
        try
        {
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

    public async Task<decimal> CalculateTotalDeductions(EmployeeInfo employee, List<AttendanceRecord> attendance,
        int year, CancellationToken cancellationToken)
    {
        try
        {
            var variables = await GetCalculationVariables(employee, attendance, year, cancellationToken);
            var totalIncome = (decimal)variables["total_income_before_deductions"];

            decimal totalDeductions = 0;

            // AFP
            var afpRate = (decimal)variables["afp_rate"];
            var afpBase = Math.Min(totalIncome, (decimal)variables["topes_afp_quincenal"]);
            totalDeductions += afpBase * (afpRate / 100m);

            // SEGURO DE SALUD
            var saludRate = (decimal)variables["salud_rate"];
            totalDeductions += afpBase * (saludRate / 100m);

            // IMPUESTO A LA RENTA
            var accumulatedIncome = (decimal)variables["accumulated_income"];
            var renta = await CalculateIncomeTax(accumulatedIncome, year);
            totalDeductions += renta;

            // APLICAR LÍMITE MÁXIMO
            var maxDeductionRate = (decimal)variables["max_deduction_rate"];
            totalDeductions = Math.Min(totalDeductions, totalIncome * (maxDeductionRate / 100m));

            _logger.LogDebug("Deducciones totales calculadas para empleado {EmployeeId} año {Year}: {TotalDeductions}",
                employee.Id, year, totalDeductions);

            return totalDeductions;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular deducciones para empleado {EmployeeId} año {Year}",
                employee.Id, year);
            return 0;
        }
    }

    private decimal CalculateSpecialHoursAmount(decimal baseSalary, decimal hours, decimal rate, decimal monthlyHours)
    {
        var hourlyRate = baseSalary / monthlyHours;
        return hours * hourlyRate * (rate / 100m);
    }

    private async Task<decimal> CalculateByFormula(PayrollFormula formula, Dictionary<string, object> variables,
        CancellationToken cancellationToken)
    {
        if (formula == null)
        {
            _logger.LogWarning("⚠️ Fórmula es nula");
            return 0;
        }

        try
        {
            _logger.LogInformation("📐 Calculando fórmula: {FormulaName}", formula.Name);
            _logger.LogDebug("📝 Expresión original: {FormulaExpression}", formula.FormulaExpression);

            var expression = formula.FormulaExpression!;

            // REEMPLAZAR VARIABLES
            foreach (var variable in variables)
            {
                string replacementValue;
                if (variable.Value is decimal decimalValue)
                {
                    replacementValue = decimalValue.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (variable.Value is int intValue)
                {
                    replacementValue = intValue.ToString();
                }
                else if (variable.Value is double doubleValue)
                {
                    replacementValue = doubleValue.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    replacementValue = variable.Value?.ToString() ?? "0";
                }

                expression = expression.Replace(variable.Key, replacementValue);
            }

            _logger.LogDebug("🔢 Expresión después de reemplazo: {Expression}", expression);

            var result = EvaluateSafeExpression(expression);
            _logger.LogInformation("✅ Fórmula {FormulaName} = {Result}", formula.Name, result);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error en fórmula {FormulaName}: {Formula}",
                formula.Name, formula.FormulaExpression);
            return 0;
        }
    }

    private decimal EvaluateSafeExpression(string expression)
    {
        try
        {
            _logger.LogDebug("🔧 Evaluando expresión: {Expression}", expression);

            // REEMPLAZAR FUNCIONES MATEMÁTICAS
            expression = expression.Replace("Math.Max", "MAX")
                                 .Replace("Math.Min", "MIN")
                                 .Replace("GREATEST", "MAX")
                                 .Replace("LEAST", "MIN");

            // IMPLEMENTAR FUNCIONES PERSONALIZADAS
            expression = ImplementCustomFunctions(expression);

            using (DataTable table = new DataTable())
            {
                try
                {
                    var result = table.Compute(expression, "");
                    var decimalResult = Convert.ToDecimal(result);
                    _logger.LogDebug("✅ Expresión evaluada: {Expression} = {Result}", expression, decimalResult);
                    return decimalResult;
                }
                catch (EvaluateException ex)
                {
                    _logger.LogWarning("⚠️ Error en DataTable.Compute, usando evaluación alternativa: {Error}", ex.Message);
                    return EvaluateWithAlternativeMethod(expression);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error en evaluación de expresión: {Expression}", expression);
            return 0;
        }
    }

    private string ImplementCustomFunctions(string expression)
    {
        // Implementar MAX manualmente
        while (expression.Contains("MAX("))
        {
            var maxStart = expression.IndexOf("MAX(");
            var maxEnd = FindMatchingParenthesis(expression, maxStart + 3);
            var maxContent = expression.Substring(maxStart + 4, maxEnd - maxStart - 4);

            var parts = maxContent.Split(',');
            if (parts.Length == 2)
            {
                var maxValue = $"IIF({parts[0]} > {parts[1]}, {parts[0]}, {parts[1]})";
                expression = expression.Replace($"MAX({maxContent})", maxValue);
            }
            else
            {
                break;
            }
        }

        return expression;
    }

    private int FindMatchingParenthesis(string expression, int startIndex)
    {
        int count = 1;
        for (int i = startIndex + 1; i < expression.Length; i++)
        {
            if (expression[i] == '(') count++;
            if (expression[i] == ')') count--;

            if (count == 0) return i;
        }
        return expression.Length - 1;
    }

    private decimal EvaluateWithAlternativeMethod(string expression)
    {
        try
        {
            _logger.LogDebug("🔄 Usando evaluación alternativa para: {Expression}", expression);

            if (expression.Contains("MAX(0,") || expression.Contains("GREATEST(0,"))
            {
                return EvaluateGreatestZeroExpression(expression);
            }

            return EvaluateSimpleArithmetic(expression);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error en evaluación alternativa: {Expression}", expression);
            return 0;
        }
    }

    private decimal EvaluateGreatestZeroExpression(string expression)
    {
        try
        {
            var start = expression.IndexOf("MAX(0,") + 6;
            var end = expression.LastIndexOf(")");
            var innerExpression = expression.Substring(start, end - start).Trim();

            var innerValue = EvaluateSimpleArithmetic(innerExpression);
            return Math.Max(0, innerValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error evaluando GREATEST(0,...): {Expression}", expression);
            return 0;
        }
    }

    private decimal EvaluateSimpleArithmetic(string expression)
    {
        try
        {
            expression = expression.Replace(" ", "");

            var parts = expression.Split('+');
            decimal result = 0;

            foreach (var part in parts)
            {
                if (part.Contains("-"))
                {
                    var subParts = part.Split('-');
                    decimal subResult = EvaluateMultiplicationDivision(subParts[0]);
                    for (int i = 1; i < subParts.Length; i++)
                    {
                        subResult -= EvaluateMultiplicationDivision(subParts[i]);
                    }
                    result += subResult;
                }
                else
                {
                    result += EvaluateMultiplicationDivision(part);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error en evaluación aritmética simple: {Expression}", expression);
            return 0;
        }
    }

    private decimal EvaluateMultiplicationDivision(string expression)
    {
        try
        {
            var parts = expression.Split('*');
            decimal result = 1;

            foreach (var part in parts)
            {
                if (part.Contains("/"))
                {
                    var divParts = part.Split('/');
                    decimal divResult = decimal.Parse(divParts[0]);
                    for (int i = 1; i < divParts.Length; i++)
                    {
                        var divisor = decimal.Parse(divParts[i]);
                        if (divisor == 0) return 0;
                        divResult /= divisor;
                    }
                    result *= divResult;
                }
                else
                {
                    result *= decimal.Parse(part);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error en multiplicación/división: {Expression}", expression);
            return 0;
        }
    }

    private decimal CalculatePercentageBase(PayrollConcept concept, Dictionary<string, object> variables)
    {
        try
        {
            var baseAmount = (decimal)variables["base_salary_quincenal"];

            if (concept.Code!.Contains("AFP") || concept.Code.Contains("SALUD") ||
                concept.Code.Contains("RENTA") || concept.Name!.Contains("ingreso"))
            {
                baseAmount = (decimal)variables["total_income"];

                if (variables.ContainsKey("topes_afp_quincenal"))
                {
                    var tope = (decimal)variables["topes_afp_quincenal"];
                    baseAmount = Math.Min(baseAmount, tope);
                }
            }

            return baseAmount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al calcular base para porcentaje del concepto {ConceptCode}", concept.Code);
            return (decimal)variables["base_salary_quincenal"];
        }
    }

    private decimal CalculateVariableConcept(PayrollConcept concept, Dictionary<string, object> variables)
    {
        try
        {
            _logger.LogDebug("🔧 Calculando concepto variable: {ConceptCode} - {ConceptName}",
                concept.Code, concept.Name);

            switch (concept.Code!.ToUpper())
            {
                case "BASICO":
                case "SUELDO_BASICO":
                    var sueldoBasico = (decimal)variables["base_salary_quincenal"];
                    _logger.LogInformation("💰 SUELDO BÁSICO calculado: S/ {Sueldo}", sueldoBasico);
                    return sueldoBasico;

                case "TARDANZA":
                case "DESCUENTO_TARDANZA":
                case "DESCUENTO_TARDANZAS":
                    var descuentoTardanza = CalculateTardanzaDiscount(variables);
                    if (descuentoTardanza > 0)
                    {
                        _logger.LogInformation("⏰ DESCUENTO POR TARDANZA calculado: S/ {Descuento}", descuentoTardanza);
                    }
                    return descuentoTardanza;

                case "GRATIFICACION":
                case "CTS":
                    // Para conceptos que son igual al sueldo básico
                    return (decimal)variables["base_salary_quincenal"];

                default:
                    _logger.LogWarning("⚠️ Concepto variable no reconocido: {ConceptCode} - {ConceptName}",
                        concept.Code, concept.Name);
                    return 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al calcular concepto variable {ConceptCode}", concept.Code);
            return 0;
        }
    }

    private decimal CalculateTardanzaDiscount(Dictionary<string, object> variables)
    {
        try
        {
            // ✅ VERSIÓN ROBUSTA: Manejar diferentes tipos
            var lateMinutesValue = variables["late_minutes"];
            decimal lateMinutes = 0;

            if (lateMinutesValue is int lateMinutesInt)
            {
                lateMinutes = (decimal)lateMinutesInt;
            }
            else if (lateMinutesValue is decimal lateMinutesDec)
            {
                lateMinutes = lateMinutesDec;
            }
            else if (lateMinutesValue is double lateMinutesDbl)
            {
                lateMinutes = (decimal)lateMinutesDbl;
            }
            else
            {
                lateMinutes = Convert.ToDecimal(lateMinutesValue);
            }

            // Si no hay minutos de tardanza, no hay descuento
            if (lateMinutes <= 0)
            {
                _logger.LogDebug("⏰ No hay descuento por tardanza - minutos: {Minutes}", lateMinutes);
                return 0;
            }

            var hourlyRate = (decimal)variables["hourly_rate"];
            var minuteRate = hourlyRate / 60m;

            var discount = lateMinutes * minuteRate;

            _logger.LogInformation("⏰ Descuento por tardanza calculado: {Minutes} minutos = S/ {Discount} (Tarifa: S/ {HourlyRate}/hora)",
                lateMinutes, discount, hourlyRate);

            return Math.Round(discount, 2);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al calcular descuento por tardanza");
            return 0;
        }
    }

    private decimal ApplyConceptLimits(PayrollConcept concept, decimal value, Dictionary<string, object> variables)
    {
        try
        {
            // ✅ LOG PARA DIAGNÓSTICO
            _logger.LogDebug("🔧 ApplyConceptLimits - Concepto: {ConceptCode}, Valor inicial: {Value}",
                concept.Code, value);

            // ✅ EXCEPCIÓN: NO APLICAR LÍMITES AL SUELDO BÁSICO
            if (concept.Code == "BASICO" || concept.Code == "SUELDO_BASICO")
            {
                _logger.LogInformation("💰 SUELDO BÁSICO - Sin aplicación de límites: {Value}", value);
                return value;
            }

            // ✅ EXCEPCIÓN: NO APLICAR LÍMITES A INGRESOS
            if (concept.ConceptTypePayrollId == 1) // INGRESOS
            {
                _logger.LogDebug("📈 CONCEPTO DE INGRESO - Sin aplicación de límites: {Value}", value);
                return value;
            }

            decimal limitedValue = value;

            // ✅ SOLO APLICAR LÍMITES A DESCUENTOS (concept_type_payroll_id = 2)
            if (concept.ConceptTypePayrollId == 2 && variables.ContainsKey("max_deduction_rate"))
            {
                var maxDeductionRate = (decimal)variables["max_deduction_rate"];
                var totalIncome = (decimal)variables["total_income"];
                var maxDeduction = totalIncome * (maxDeductionRate / 100m);

                _logger.LogDebug("📉 LÍMITE DE DESCUENTO - Concepto: {ConceptCode}, Valor: {Value}, Máximo: {Max}",
                    concept.Code, value, maxDeduction);

                if (value > maxDeduction)
                {
                    limitedValue = maxDeduction;
                    _logger.LogInformation("📉 APLICANDO LÍMITE - {ConceptCode}: {Original} → {Limited}",
                        concept.Code, value, limitedValue);
                }
            }

            _logger.LogDebug("✅ ApplyConceptLimits - Concepto: {ConceptCode}, Valor final: {Value}",
                concept.Code, limitedValue);

            return Math.Max(0, limitedValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al aplicar límites al concepto {ConceptCode}", concept.Code);
            return Math.Max(0, value);
        }
    }

    private async Task<decimal> CalculateIncomeTax(decimal accumulatedIncome, int year)
    {
        var uit = await GetParameterByYear("UIT", year, 4950.00m);
        var exemptLimit = 5 * uit;

        if (accumulatedIncome <= exemptLimit)
            return 0;

        var taxableIncome = accumulatedIncome - exemptLimit;
        return taxableIncome * 0.08m;
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

    private decimal CalculateAccumulatedIncome(EmployeeInfo employee)
    {
        var months = CalculateAntiquity(employee.HireDate);
        return employee.BaseSalary * Math.Min(months, 12);
    }

    private async Task<decimal> CalculateAccumulatedAFP(EmployeeInfo employee, int year, CancellationToken cancellationToken)
    {
        var accumulatedIncome = CalculateAccumulatedIncome(employee);
        var topeAfpMensual = await GetParameterByYear("TOPE_AFP_MENSUAL", year, 8955.00m) * 12;
        var afpBase = Math.Min(accumulatedIncome, topeAfpMensual);
        var afpRate = (await _legalParameters.GetParametersByCategory("DESCUENTOS",
            DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.Date)))
            .GetValueOrDefault("AFP_TRABAJADOR", 10.00m);

        return afpBase * (afpRate / 100m);
    }

    private async Task<decimal> CalculateAccumulatedRenta(EmployeeInfo employee, int year, CancellationToken cancellationToken)
    {
        var accumulatedIncome = CalculateAccumulatedIncome(employee);
        var uit = await GetParameterByYear("UIT", year, 4950.00m);
        var rentaBase = Math.Max(0, accumulatedIncome - (5 * uit));
        return rentaBase * 0.08m;
    }

    private bool IsConceptForSpecificEmployee(PayrollConcept concept, long employeeId)
    {
        return true;
    }
}