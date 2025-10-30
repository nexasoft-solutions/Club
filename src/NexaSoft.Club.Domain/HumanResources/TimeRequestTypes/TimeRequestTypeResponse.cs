namespace NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;

public sealed record TimeRequestTypeResponse(
    long Id,
    string? Code,
    string? Name,
    bool DeductsSalary,
    bool RequiresApproval,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
