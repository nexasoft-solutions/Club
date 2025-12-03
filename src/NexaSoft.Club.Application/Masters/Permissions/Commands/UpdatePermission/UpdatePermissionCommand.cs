using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.CreatePermission;

public sealed record UpdatePermissionCommand
(
    long Id,
    string? Name,
    string? Description,
    string? Reference,
    string? Source,
    string? Action,
    string? UpdatedBy
):ICommand<bool>;