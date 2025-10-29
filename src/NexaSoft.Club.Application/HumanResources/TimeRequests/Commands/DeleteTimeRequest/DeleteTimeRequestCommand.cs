using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.DeleteTimeRequest;

public sealed record DeleteTimeRequestCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
