using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.DeleteMenu;

public sealed record  DeleteMenuCommand
(
    Guid Id
):ICommand<bool>;