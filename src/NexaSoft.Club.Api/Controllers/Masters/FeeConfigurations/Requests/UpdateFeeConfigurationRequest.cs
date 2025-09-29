namespace NexaSoft.Club.Api.Controllers.Masters.FeeConfigurations.Request;

public sealed record UpdateFeeConfigurationRequest(
    long Id,
    string? FeeName,
    long PeriodicityId,
    int? DueDay,
    decimal? DefaultAmount,
    bool IsVariable,
    int Priority,
    bool AppliesToFamily,
    long? IncomeAccountId,
    string UpdatedBy
);
