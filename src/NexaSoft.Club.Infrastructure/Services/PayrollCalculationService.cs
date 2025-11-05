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
using static NexaSoft.Club.Domain.Shareds.Enums;

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
    public async Task<decimal> CalculateConceptValue(
        PayrollConcept concept,
        EmployeeInfoResponse employee,
        List<AttendanceRecord> attendance,
        int year,
        //PayrollPeriod? payrollPeriod,
        long? payrollTypeId,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Calculando concepto {ConceptCode} para empleado {EmployeeId} año {Year}", concept.Code, employee.Id, year);

            // Obtener fórmula asociada si existe
            var formula = concept.PayrollFormula;

            // Obtener variables dinámicamente según la fórmula y el concepto
            var variables = await GetDynamicVariablesAsync(concept, formula, employee, attendance, year, payrollTypeId, cancellationToken);

            // Verificar si el concepto aplica
            if (!await ConceptAppliesToEmployee(concept, employee, variables, cancellationToken))
            {
                _logger.LogDebug("Concepto {ConceptCode} no aplica al empleado {EmployeeId}", concept.Code, employee.Id);
                return 0;
            }

            decimal calculatedValue = 0;

            // Cálculo según tipo
            switch (concept.ConceptCalculationTypeId)
            {
                case 1: // FIJO
                    calculatedValue = concept.FixedValue ?? 0;
                    _logger.LogDebug("Concepto {ConceptCode} calculado como FIJO: {Value}", concept.Code, calculatedValue);
                    break;

                case 2: // PORCENTAJE
                    // Si la fórmula define la base, úsala, si no, usa base_salary_quincenal
                    var baseAmount = variables.ContainsKey("base_amount") ? (decimal)variables["base_amount"] : (decimal)variables.GetValueOrDefault("base_salary_quincenal", 0m);
                    var porcentaje = concept.PorcentajeValue ?? 0;
                    calculatedValue = baseAmount * (porcentaje / 100m);
                    _logger.LogDebug("Concepto {ConceptCode} calculado como PORCENTAJE: {Value} (base: {Base}, %: {Percentage})", concept.Code, calculatedValue, baseAmount, porcentaje);
                    break;

                case 3: // FORMULA
                    if (formula != null)
                    {
                        calculatedValue = await CalculateByFormula(formula, variables, cancellationToken);
                        _logger.LogDebug("Concepto {ConceptCode} calculado por FÓRMULA: {Value}", concept.Code, calculatedValue);
                    }
                    else
                    {
                        _logger.LogWarning("Concepto {ConceptCode} tiene tipo FORMULA pero no tiene fórmula asociada", concept.Code);
                    }
                    break;

                case 4: // VARIABLE
                    // Si la variable existe, úsala, si no, fallback a 0
                    var varKey = concept.Code?.ToLower() ?? string.Empty;
                    if (!string.IsNullOrEmpty(varKey) && variables.TryGetValue(varKey, out var varValue) && varValue != null)
                    {
                        calculatedValue = Convert.ToDecimal(varValue);
                    }
                    else
                    {
                        calculatedValue = 0m;
                    }
                    _logger.LogDebug("Concepto {ConceptCode} calculado como VARIABLE: {Value}", concept.Code, calculatedValue);
                    break;

                default:
                    _logger.LogWarning("Tipo de cálculo no reconocido para concepto {ConceptCode}", concept.Code);
                    break;
            }

            // Aplicar límites configurables
            calculatedValue = ApplyConceptLimits(concept, calculatedValue, variables);

            _logger.LogInformation("Concepto {ConceptCode} calculado: {Value} para empleado {EmployeeId}", concept.Code, calculatedValue, employee.Id);

            return Math.Max(0, calculatedValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error crítico al calcular concepto {ConceptCode} para empleado {EmployeeId} año {Year}", concept.Code, employee.Id, year);
            return 0;
        }
    }


    // Implementación original para compatibilidad con la interfaz
    public async Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfoResponse employee,
        List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken)
    {
        // Llama a la sobrecarga dinámica con concept y formula en null
        return await GetCalculationVariables(employee, attendance, year, cancellationToken, null, null);
    }

    // Nueva sobrecarga dinámica
    public async Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfoResponse employee,
        List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken,
        PayrollConcept? concept, PayrollFormula? formula)
    {
        return await GetDynamicVariablesAsync(
            concept,
            formula,
            employee,
            attendance,
            year,
            null,
            cancellationToken
        );
    }



    // ✅ CORREGIDO: Solo calcular ingresos adicionales (NO incluir sueldo básico)
    private decimal CalculateIngresosAdicionales(EmployeeInfoResponse employee, Dictionary<string, object> variables)
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
    private decimal CalculateTotalIncomeForFormulas(EmployeeInfoResponse employee, Dictionary<string, object> variables)
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

    // Obtiene variables dinámicamente según la lista de variables requeridas por la fórmula del concepto
    private async Task<Dictionary<string, object>> GetDynamicVariablesAsync(
        PayrollConcept? concept,
        PayrollFormula? formula,
        EmployeeInfoResponse employee,
        List<AttendanceRecord> attendance,
        int year,
        long? payrollTypeId,
        CancellationToken cancellationToken)
    {
        var variables = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        var calculationDate = DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.Date);

        // Detectar tipo de planilla (quincenal/mensual) usando PayrollPeriod
        //var payrollTypeCode = payrollPeriod?.PayrollTypeId ?? 2;

        // Resolutor de sueldo base según tipo de planilla
        Func<decimal> sueldoBaseResolver = () =>
        {
            if (payrollTypeId == (int)PayRollTypesEnum.Quincenal)
                return employee.BaseSalary / 2;
            return employee.BaseSalary;
        };

        // Diccionario de resolutores genéricos
        var resolvers = new Dictionary<string, Func<Task<object>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["employee_id"] = () => Task.FromResult<object>(employee.Id),
            ["base_salary"] = () => Task.FromResult<object>(employee.BaseSalary),
            ["base_salary_quincenal"] = () => Task.FromResult<object>(sueldoBaseResolver()),
            ["base_salary_diario"] = () => Task.FromResult<object>(employee.BaseSalary / 30),
            // ...otros resolutores...
            // Resolutores especiales para conceptos tipo VARIABLE sin fórmula:
            ["sueldo_base"] = () => Task.FromResult<object>(sueldoBaseResolver()),
            ["sueldo_basico"] = () => Task.FromResult<object>(sueldoBaseResolver()),
            ["basico"] = () => Task.FromResult<object>(sueldoBaseResolver()),
            ["tardanza"] = async () =>
            {
                var minutosTardanza = attendance.Sum(a => a.LateMinutes) ?? 0;
                var horasMensuales = await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES");
                var valorMinuto = (employee.BaseSalary / (decimal)horasMensuales) / 60m;
                return Math.Round(minutosTardanza * valorMinuto, 2);
            },
            ["late_hours"] = () => Task.FromResult<object>(attendance.Sum(a => a.LateMinutes) / 60m ?? 0),
            ["early_departure_minutes"] = () => Task.FromResult<object>(attendance.Sum(a => a.EarlyDepartureMinutes) ?? 0),
            ["attendance_rate"] = () => Task.FromResult<object>(CalculateAttendanceRate(attendance)),
            ["uit"] = async () => await GetParameterByYear("UIT", year, 4950.00m),
            ["remuneracion_minima"] = async () => await GetParameterByYear("RMV", year, 1025.00m),
            ["topes_afp_quincenal"] = async () => await GetParameterByYear("TOPE_AFP_QUINCENAL", year, 4477.50m),
            ["topes_afp"] = async () => await GetParameterByYear("TOPE_AFP_MENSUAL", year, 8955.00m),
            ["monthly_hours"] = async () => await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"),
            ["uit_x_5"] = async () => (decimal)await GetParameterByYear("UIT", year, 4950.00m) * 5,
            ["year"] = () => Task.FromResult<object>(year),
            ["afp_rate"] = async () => (await _legalParameters.GetParametersByCategory("DESCUENTOS", calculationDate)).GetValueOrDefault("AFP_TRABAJADOR", 10.00m),
            ["salud_rate"] = async () => (await _legalParameters.GetParametersByCategory("DESCUENTOS", calculationDate)).GetValueOrDefault("SALUD_TRABAJADOR", 5.00m),
            ["max_deduction_rate"] = async () => (await _legalParameters.GetParametersByCategory("DESCUENTOS", calculationDate)).GetValueOrDefault("DESCUENTO_MAX_PORCENTAJE", 50.00m),
            ["adelanto_max_rate"] = async () => (await _legalParameters.GetParametersByCategory("DESCUENTOS", calculationDate)).GetValueOrDefault("ADELANTO_MAX_PORCENTAJE", 30.00m),
            ["hourly_rate"] = async () => employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES")),
            ["overtime_amount"] = async () => ((decimal)(attendance.Sum(a => a.OvertimeHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 1.5m,
            ["sunday_amount"] = async () => ((decimal)(attendance.Sum(a => a.SundayHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 2.0m,
            ["holiday_amount"] = async () => ((decimal)(attendance.Sum(a => a.HolidayHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 2.5m,
            ["night_amount"] = async () => ((decimal)(attendance.Sum(a => a.NightHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 1.25m,
            ["total_income"] = async () => sueldoBaseResolver()
                + ((decimal)(attendance.Sum(a => a.OvertimeHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 1.5m
                + ((decimal)(attendance.Sum(a => a.SundayHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 2.0m
                + ((decimal)(attendance.Sum(a => a.HolidayHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 2.5m
                + ((decimal)(attendance.Sum(a => a.NightHours) ?? 0)) * (employee.BaseSalary / (decimal)(await _legalParameters.GetCurrentParameterValue("HORAS_MENSUALES"))) * 1.25m,
            ["accumulated_income"] = () => Task.FromResult<object>(CalculateAccumulatedIncome(employee)),
            ["accumulated_afp"] = async () => await CalculateAccumulatedAFP(employee, year, cancellationToken),
            ["accumulated_renta"] = async () => await CalculateAccumulatedRenta(employee, year, cancellationToken),
        };

        // 1. Leer variables requeridas desde el campo JSON de la fórmula
        var requiredVariables = new List<string>();
        if (formula?.RequiredVariables != null)
        {
            requiredVariables = System.Text.Json.JsonSerializer.Deserialize<List<string>>(formula.RequiredVariables) ?? new List<string>();
        }

        // 2. Si el concepto no tiene fórmula, agregar la variable con el mismo nombre que el código (en minúsculas)
        if (formula == null && concept != null && !string.IsNullOrEmpty(concept.Code))
        {
            var varKey = concept.Code.ToLower();
            if (!requiredVariables.Contains(varKey))
                requiredVariables.Add(varKey);
        }

        // 3. Resolver solo las variables requeridas
        foreach (var varName in requiredVariables)
        {
            if (resolvers.TryGetValue(varName, out var resolver))
                variables[varName] = await resolver();
            else
                variables[varName] = 0; // o log de advertencia si la variable no está implementada
        }

        return variables;
    }

    public async Task<decimal> CalculateTotalIncome(EmployeeInfoResponse employee, List<AttendanceRecord> attendance,
        int year, CancellationToken cancellationToken)
    {
        var variables = await GetCalculationVariables(employee, attendance, year, cancellationToken);
        return (decimal)variables["total_income_before_deductions"];
    }

    public async Task<bool> ConceptAppliesToEmployee(PayrollConcept concept, EmployeeInfoResponse employee,
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

    public async Task<decimal> CalculateTotalDeductions(EmployeeInfoResponse employee, List<AttendanceRecord> attendance,
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

    private decimal CalculateAccumulatedIncome(EmployeeInfoResponse employee)
    {
        var months = CalculateAntiquity(employee.HireDate);
        return employee.BaseSalary * Math.Min(months, 12);
    }

    private async Task<decimal> CalculateAccumulatedAFP(EmployeeInfoResponse employee, int year, CancellationToken cancellationToken)
    {
        var accumulatedIncome = CalculateAccumulatedIncome(employee);
        var topeAfpMensual = await GetParameterByYear("TOPE_AFP_MENSUAL", year, 8955.00m) * 12;
        var afpBase = Math.Min(accumulatedIncome, topeAfpMensual);
        var afpRate = (await _legalParameters.GetParametersByCategory("DESCUENTOS",
            DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.Date)))
            .GetValueOrDefault("AFP_TRABAJADOR", 10.00m);

        return afpBase * (afpRate / 100m);
    }

    private async Task<decimal> CalculateAccumulatedRenta(EmployeeInfoResponse employee, int year, CancellationToken cancellationToken)
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