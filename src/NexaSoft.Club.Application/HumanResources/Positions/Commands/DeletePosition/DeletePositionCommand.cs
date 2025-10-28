using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Positions.Commands.DeletePosition;

public sealed record DeletePositionCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
