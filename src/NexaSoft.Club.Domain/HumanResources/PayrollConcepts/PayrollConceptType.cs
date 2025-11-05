using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

public class PayrollConceptType : Entity
{
    public long PayrollConceptId { get; private set; }
    public long PayrollTypeId { get; private set; }
    public PayrollConcept? PayrollConcept { get; private set; }
    public PayrollType? PayrollType { get; private set; }
    private PayrollConceptType() { }
    private PayrollConceptType(
        long payrollConceptId,
        long payrollTypeId,
        DateTime createdAt,
        string? createdBy
    ) : base(createdAt, createdBy)
    {
        PayrollConceptId = payrollConceptId;
        PayrollTypeId = payrollTypeId;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public static PayrollConceptType Create(
        long payrollConceptId,
        long payrollTypeId,
        DateTime createdAt,
        string? createdBy
    )
    {
        return new PayrollConceptType(
            payrollConceptId,
            payrollTypeId,
            createdAt,  
            createdBy
        );
    }
}
