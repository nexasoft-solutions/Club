namespace NexaSoft.Club.Domain.Masters.FeeConfigurations;

public sealed record FeeConfigurationResponse(
    long Id,
    string? FeeName, 
    long PeriodicityId, 
    string? Name,
    int? DueDay, 
    decimal? DefaultAmount,
    bool IsVariable,
    int Priority,
    bool AppliesToFamily,
    long? IncomeAccountId,
    string? AccountName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
