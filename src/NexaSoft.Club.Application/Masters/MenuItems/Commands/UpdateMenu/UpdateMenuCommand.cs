using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MenuItems.Commands.UpdateMenu;

public sealed record UpdateMenuCommand
(
    long Id,
    string? Label,
    string? Icon,
    string? Route,
    string? UsuarioModificacion
):ICommand<bool>;
