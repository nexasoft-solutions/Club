
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.RevokePermissionFromRole;

public sealed record RevokePermissionFromRoleCommand(Guid RoleId, List<Guid> PermissionIds) 
    : ICommand<bool>;

