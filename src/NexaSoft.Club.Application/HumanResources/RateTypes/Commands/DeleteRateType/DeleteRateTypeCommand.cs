using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Commands.DeleteRateType;

public sealed record DeleteRateTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
