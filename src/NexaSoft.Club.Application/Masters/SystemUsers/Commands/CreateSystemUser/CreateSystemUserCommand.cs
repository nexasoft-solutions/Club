using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Commands.CreateSystemUser;

public sealed record CreateSystemUserCommand(
    string? Username,
    string? Email,
    string? FirstName,
    string? LastName,
    string? Role,
    bool IsActive,
    string CreatedBy
) : ICommand<long>;
