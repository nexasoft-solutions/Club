namespace NexaSoft.Club.Api.Controllers.Masters.Users.Request;

public sealed record UpdateUserRequest(
   long Id,
    string? UserApellidos,
    string? UserNombres,
    string? Password,
    string? Email,
    string? UserDni,
    string? UserTelefono,
    string? UsuarioModificacion
);
