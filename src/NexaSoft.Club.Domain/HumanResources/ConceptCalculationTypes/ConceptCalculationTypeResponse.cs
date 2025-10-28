namespace NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;

public sealed record ConceptCalculationTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
