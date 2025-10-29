namespace NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;

public sealed record ConceptTypePayrollResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
