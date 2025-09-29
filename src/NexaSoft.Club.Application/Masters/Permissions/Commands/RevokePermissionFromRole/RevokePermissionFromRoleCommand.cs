
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.RevokePermissionFromRole;

public sealed record RevokePermissionFromRoleCommand(long RoleId, List<long> PermissionIds) 
    : ICommand<bool>;

