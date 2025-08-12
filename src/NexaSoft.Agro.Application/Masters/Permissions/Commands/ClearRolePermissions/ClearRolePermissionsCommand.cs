using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.ClearRolePermissions;

public sealed record  ClearRolePermissionsCommand(Guid RoleId) : ICommand<bool>;
