using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;
using NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;
using NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

public class PayrollConcept : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public long? ConceptTypePayrollId { get; private set; }
    public ConceptTypePayroll? ConceptTypePayroll { get; private set; }
    public long? PayrollFormulaId { get; private set; }
    public PayrollFormula? PayrollFormula { get; private set; }
    public long? ConceptCalculationTypeId { get; private set; }
    public ConceptCalculationType? ConceptCalculationType { get; private set; }
    public decimal FixedValue { get; private set; }
    public decimal PorcentajeValue { get; private set; }
    public long? ConceptApplicationTypesId { get; private set; }
    public ConceptApplicationType? ConceptApplicationType { get; private set; }
    public long? AccountingChartId { get; private set; }
    public AccountingChart? AccountingChart { get; private set; }
    public int StatePayrollConcept { get; private set; }

    private PayrollConcept() { }

    private PayrollConcept(
        string? code, 
        string? name, 
        long? conceptTypePayrollId, 
        long? payrollFormulaId, 
        long? conceptCalculationTypeId, 
        decimal fixedValue, 
        decimal porcentajeValue, 
        long? conceptApplicationTypesId, 
        long? accountingChartId, 
        int statePayrollConcept, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        ConceptTypePayrollId = conceptTypePayrollId;
        PayrollFormulaId = payrollFormulaId;
        ConceptCalculationTypeId = conceptCalculationTypeId;
        FixedValue = fixedValue;
        PorcentajeValue = porcentajeValue;
        ConceptApplicationTypesId = conceptApplicationTypesId;
        AccountingChartId = accountingChartId;
        StatePayrollConcept = statePayrollConcept;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollConcept Create(
        string? code, 
        string? name, 
        long? conceptTypePayrollId, 
        long? payrollFormulaId, 
        long? conceptCalculationTypeId, 
        decimal fixedValue, 
        decimal porcentajeValue, 
        long? conceptApplicationTypesId, 
        long? accountingChartId, 
        int statePayrollConcept, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollConcept(
            code,
            name,
            conceptTypePayrollId,
            payrollFormulaId,
            conceptCalculationTypeId,
            fixedValue,
            porcentajeValue,
            conceptApplicationTypesId,
            accountingChartId,
            statePayrollConcept,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        long? conceptTypePayrollId, 
        long? payrollFormulaId, 
        long? conceptCalculationTypeId, 
        decimal fixedValue, 
        decimal porcentajeValue, 
        long? conceptApplicationTypesId, 
        long? accountingChartId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        ConceptTypePayrollId = conceptTypePayrollId;
        PayrollFormulaId = payrollFormulaId;
        ConceptCalculationTypeId = conceptCalculationTypeId;
        FixedValue = fixedValue;
        PorcentajeValue = porcentajeValue;
        ConceptApplicationTypesId = conceptApplicationTypesId;
        AccountingChartId = accountingChartId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollConcept = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
