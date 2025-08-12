using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Roles.Commands.CreateRole;

public sealed record UpdateRoleCommand
(
    Guid Id,
    string? Name,
    string? Description
):ICommand<bool>;