using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string? UserApellidos,
    string? UserNombres,
    string? Password,
    string? Email,
    string? UserDni,
    string? UserTelefono
) : ICommand<long>;
