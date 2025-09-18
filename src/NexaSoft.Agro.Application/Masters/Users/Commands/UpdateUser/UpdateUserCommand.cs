using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    long Id,
    string? UserApellidos,
    string? UserNombres, 
    string? Password,
    string? Email,
    string? UserDni,
    string? UserTelefono,
    string? UsuarioModificacion
) : ICommand<bool>;
