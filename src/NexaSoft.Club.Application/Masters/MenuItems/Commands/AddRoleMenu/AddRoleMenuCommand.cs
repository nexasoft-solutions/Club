using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MenuItems.Commands.AddRoleMenu;

public sealed record AddRoleMenuCommand(
    long MenuId,
    List<long> RoleIds
):ICommand<bool>;

