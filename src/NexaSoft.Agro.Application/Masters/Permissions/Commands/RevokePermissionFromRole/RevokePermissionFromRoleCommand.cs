
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.RevokePermissionFromRole;

public sealed record RevokePermissionFromRoleCommand(long RoleId, List<long> PermissionIds) 
    : ICommand<bool>;

