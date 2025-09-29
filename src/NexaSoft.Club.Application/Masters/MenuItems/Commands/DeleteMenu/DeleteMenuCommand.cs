using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MenuItems.Commands.DeleteMenu;

public sealed record  DeleteMenuCommand
(
    long Id,
    string UsuarioEliminacion
):ICommand<bool>;