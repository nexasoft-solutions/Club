using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Commands.DeleteSystemUser;

public sealed record DeleteSystemUserCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
