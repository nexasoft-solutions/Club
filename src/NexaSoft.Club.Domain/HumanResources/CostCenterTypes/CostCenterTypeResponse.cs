namespace NexaSoft.Club.Domain.HumanResources.CostCenterTypes;

public sealed record CostCenterTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
