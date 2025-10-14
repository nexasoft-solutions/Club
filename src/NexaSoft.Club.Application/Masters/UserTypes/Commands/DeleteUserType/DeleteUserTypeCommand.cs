using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.UserTypes.Commands.DeleteUserType;

public sealed record DeleteUserTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
