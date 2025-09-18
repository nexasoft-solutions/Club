using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.AssignPermissionToRole;

public record class AssignPermissionToRoleCommand(long RoleId, List<long> PermissionIds)
    : ICommand<bool>;
