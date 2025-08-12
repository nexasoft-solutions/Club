using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.AssignPermissionToRole;

public record class AssignPermissionToRoleCommand(Guid RoleId, List<Guid> PermissionIds)
    : ICommand<bool>;
