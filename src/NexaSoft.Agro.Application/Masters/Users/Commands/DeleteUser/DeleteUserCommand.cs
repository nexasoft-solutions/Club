using Microsoft.Win32.SafeHandles;
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
