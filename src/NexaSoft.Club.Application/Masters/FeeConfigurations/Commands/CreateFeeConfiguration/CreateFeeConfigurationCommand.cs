using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.CreateFeeConfiguration;

public sealed record CreateFeeConfigurationCommand(
    string? FeeName,
    long PeriodicityId,
    int? DueDay,
    decimal? DefaultAmount,
    bool IsVariable,
    int Priority,
    bool AppliesToFamily,
    long? IncomeAccountId,
    string CreatedBy
) : ICommand<long>;
