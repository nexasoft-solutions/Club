using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand
(
    string? Name,
    string? Description
):ICommand<Guid>;