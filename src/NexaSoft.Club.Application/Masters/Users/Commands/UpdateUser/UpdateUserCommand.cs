using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    long Id,
    string? LastName,
    string? FirstName,
    string? Email,
    string? Dni,
    string? Phone,
    long UserTypeId,
    DateOnly BirthDate,
    long? MemberId,
    string? UserModification
) : ICommand<bool>;
