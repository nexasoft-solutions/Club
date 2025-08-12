namespace NexaSoft.Agro.Domain.Masters.Users;

public sealed record UserResponse(
    Guid Id,
    string? UserApellidos,
    string? UserNombres,
    string? NombreCompleto,
    string? UserName,
    string? Email,
    string? UserDni,
    string? UserTelefono,
    DateTime FechaCreacion
);
