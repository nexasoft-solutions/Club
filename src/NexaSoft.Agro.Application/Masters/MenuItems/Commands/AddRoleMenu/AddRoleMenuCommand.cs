using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.AddRoleMenu;

public sealed record AddRoleMenuCommand(
    long MenuId,
    List<long> RoleIds
):ICommand<bool>;

