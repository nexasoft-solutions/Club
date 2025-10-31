namespace NexaSoft.Club.Domain.HumanResources.PayrollTypes;

public sealed record PayrollTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
