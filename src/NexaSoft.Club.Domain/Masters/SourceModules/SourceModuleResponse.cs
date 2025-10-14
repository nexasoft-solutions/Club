namespace NexaSoft.Club.Domain.Masters.SourceModules;

public sealed record SourceModuleResponse(
    long Id,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
