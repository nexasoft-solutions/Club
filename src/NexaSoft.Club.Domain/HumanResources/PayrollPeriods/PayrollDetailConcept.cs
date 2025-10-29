using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public class PayrollDetailConcept: Entity
{
    public long PayrollDetailId { get; private set; }
    public long ConceptId { get; private set; }

    public decimal? Amount { get; private set; }
    public decimal? Quantity { get; private set; }
    public decimal? CalculatedValue { get; private set; }
    public string? Description { get; private set; }

    public PayrollDetail? PayrollDetail { get; private set; }
    public PayrollConcept? Concept { get; private set; }

    public int StatePayrollDetailConcept { get; private set; }

    private PayrollDetailConcept() { }

    private PayrollDetailConcept(
        long payrollDetailId,
        long conceptId,
        decimal? amount,
        decimal? quantity,
        decimal? calculatedValue,
        string? description,
        int statePayrollDetailConcept,
        DateTime createdAt,
        string? createdBy
    ) : base(createdAt, createdBy)
    {
        PayrollDetailId = payrollDetailId;
        ConceptId = conceptId;
        Amount = amount;
        Quantity = quantity;
        CalculatedValue = calculatedValue;
        Description = description;
        StatePayrollDetailConcept = statePayrollDetailConcept;
    }

    public static PayrollDetailConcept Create(
        long payrollDetailId,
        long conceptId,
        decimal? amount,
        decimal? quantity,
        decimal? calculatedValue,
        string? description,
        int statePayrollDetailConcept,
        DateTime createdAt,
        string? createdBy
    )
    {
        return new PayrollDetailConcept(
            payrollDetailId,
            conceptId,
            amount,
            quantity,
            calculatedValue,
            description,
            statePayrollDetailConcept,
            createdAt,
            createdBy
        );
    }

    /*public Result Update(
        decimal? amount,
        decimal? quantity,
        decimal? calculatedValue,
        string? description,
        DateTime utcNow,
        string? updatedBy
    )
    {
        Amount = amount;
        Quantity = quantity;
        CalculatedValue = calculatedValue;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;
        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StatePayrollDetailConcept = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }*/
}
