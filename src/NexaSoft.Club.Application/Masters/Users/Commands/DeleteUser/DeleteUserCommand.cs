using Microsoft.Win32.SafeHandles;
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
