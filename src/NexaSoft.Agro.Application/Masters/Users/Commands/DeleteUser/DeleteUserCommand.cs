using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(
    Guid Id
) : ICommand<bool>;
