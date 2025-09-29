using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Commands.UpdateSystemUser;

public sealed record UpdateSystemUserCommand(
    long Id,
    string? Username,
    string? Email,
    string? FirstName,
    string? LastName,
    string? Role,
    bool IsActive,
    string UpdatedBy
) : ICommand<bool>;
