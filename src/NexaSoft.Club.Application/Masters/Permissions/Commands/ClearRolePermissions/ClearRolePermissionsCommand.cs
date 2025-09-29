using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.ClearRolePermissions;

public sealed record  ClearRolePermissionsCommand(long RoleId) : ICommand<bool>;
