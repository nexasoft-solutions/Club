namespace NexaSoft.Agro.Api.Controllers.Masters.Users.Requests;

public record class DeleteUserRequest
(
    long Id,
    string UsuarioEliminacion
);
