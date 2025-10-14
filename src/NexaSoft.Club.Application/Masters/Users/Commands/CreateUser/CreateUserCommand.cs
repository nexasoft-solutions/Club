using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string? LastName,
    string? FirstName,
    string? Email,
    string? Dni,
    string? Phone,
    long UserTypeId,
    DateOnly BirthDate,
    long? MemberId
) : ICommand<long>;
