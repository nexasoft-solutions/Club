using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string? UserApellidos,
    string? UserNombres, 
    string? Password,
    string? Email,
    string? UserDni,
    string? UserTelefono
) : ICommand<bool>;
