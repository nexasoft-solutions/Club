using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.AddRoleMenu;

public sealed record AddRoleMenuCommand(
    Guid MenuId,
    List<Guid> RoleIds
):ICommand<bool>;

