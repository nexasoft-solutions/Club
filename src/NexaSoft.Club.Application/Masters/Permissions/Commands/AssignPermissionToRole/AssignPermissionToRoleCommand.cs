using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.AssignPermissionToRole;

public record class AssignPermissionToRoleCommand(long RoleId, List<long> PermissionIds)
    : ICommand<bool>;
