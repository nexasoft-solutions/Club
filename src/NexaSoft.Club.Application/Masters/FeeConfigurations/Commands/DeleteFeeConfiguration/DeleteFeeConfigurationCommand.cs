using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.DeleteFeeConfiguration;

public sealed record DeleteFeeConfigurationCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
