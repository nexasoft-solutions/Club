namespace NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;

public sealed record PayrollStatusTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
