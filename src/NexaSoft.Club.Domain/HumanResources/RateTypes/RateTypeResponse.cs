namespace NexaSoft.Club.Domain.HumanResources.RateTypes;

public sealed record RateTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
