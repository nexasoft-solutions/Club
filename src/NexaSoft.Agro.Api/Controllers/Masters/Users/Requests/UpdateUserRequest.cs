namespace NexaSoft.Agro.Api.Controllers.Masters.Users.Request;

public sealed record UpdateUserRequest(
   Guid Id,
    string? UserApellidos,
    string? UserNombres,
    string? Password,
    string? Email,
    string? UserDni,
    string? UserTelefono
);
