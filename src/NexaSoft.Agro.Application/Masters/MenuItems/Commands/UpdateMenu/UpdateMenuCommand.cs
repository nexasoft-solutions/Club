using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.UpdateMenu;

public sealed record UpdateMenuCommand
(
    Guid Id,
    string? Label,
    string? Icon,
    string? Route   
):ICommand<bool>;
