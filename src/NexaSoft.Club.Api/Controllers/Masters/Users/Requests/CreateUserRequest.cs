namespace NexaSoft.Club.Api.Controllers.Masters.Users.Request;

public sealed record CreateUserRequest(
    string? UserApellidos,
    string? UserNombres,  
    string? Password,
    string? Email,
    string? UserDni,
    string? UserTelefono
);
