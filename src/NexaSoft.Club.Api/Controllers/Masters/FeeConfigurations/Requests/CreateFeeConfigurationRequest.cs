namespace NexaSoft.Club.Api.Controllers.Masters.FeeConfigurations.Request;

public sealed record CreateFeeConfigurationRequest(
    string? FeeName,
    long PeriodicityId,
    int? DueDay,
    decimal? DefaultAmount,
    bool IsVariable,
    int Priority,
    bool AppliesToFamily,
    long? IncomeAccountId,
    string CreatedBy
);
