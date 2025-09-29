using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.UpdateFeeConfiguration;

public sealed record UpdateFeeConfigurationCommand(
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
) : ICommand<bool>;
