
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.CreateMenu;

public sealed record CreateMenuCommand
(
    string? Label,
    string? Icon,
    string? Route,
    Guid? ParentId
):ICommand<Guid>;