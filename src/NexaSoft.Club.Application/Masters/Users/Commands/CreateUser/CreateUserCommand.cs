using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string? UserApellidos,
    string? UserNombres,
    string? Password,
    string? Email,
    string? UserDni,
    string? UserTelefono
) : ICommand<long>;
