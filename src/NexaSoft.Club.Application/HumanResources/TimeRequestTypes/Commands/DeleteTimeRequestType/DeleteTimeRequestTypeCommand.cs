using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.DeleteTimeRequestType;

public sealed record DeleteTimeRequestTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
