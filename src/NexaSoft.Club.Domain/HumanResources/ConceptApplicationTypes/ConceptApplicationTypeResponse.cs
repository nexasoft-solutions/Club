namespace NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;

public sealed record ConceptApplicationTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
