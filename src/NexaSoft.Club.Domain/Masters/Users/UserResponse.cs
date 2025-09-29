namespace NexaSoft.Club.Domain.Masters.Users;

public sealed record UserResponse(
    long Id,
    string? UserApellidos,
    string? UserNombres,
    string? NombreCompleto,
    string? UserName,
    string? Email,
    string? UserDni,
    string? UserTelefono,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
