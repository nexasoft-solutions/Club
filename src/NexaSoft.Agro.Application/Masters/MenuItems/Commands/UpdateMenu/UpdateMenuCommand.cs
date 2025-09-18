using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.UpdateMenu;

public sealed record UpdateMenuCommand
(
    long Id,
    string? Label,
    string? Icon,
    string? Route,
    string? UsuarioModificacion
):ICommand<bool>;
