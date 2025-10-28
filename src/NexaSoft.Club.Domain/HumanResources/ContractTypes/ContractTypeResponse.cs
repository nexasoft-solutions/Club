namespace NexaSoft.Club.Domain.HumanResources.ContractTypes;

public sealed record ContractTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
