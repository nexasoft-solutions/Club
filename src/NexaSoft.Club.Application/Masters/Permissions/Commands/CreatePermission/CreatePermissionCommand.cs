using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.CreatePermision;

public sealed record CreatePermissionCommand
(
    string? Name,
    string? Description,
    string? Reference,
    string? Source,
    string? Action,
    string? CreatedBy
):ICommand<long>;