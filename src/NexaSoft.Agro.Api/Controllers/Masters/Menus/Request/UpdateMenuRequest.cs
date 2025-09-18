namespace NexaSoft.Agro.Api.Controllers.Masters.Menus.Request;

public sealed record UpdateMenuRequest
(
    long Id,
    string? Label,
    string? Icon,
    string? Route,
    string? UsuarioModificacion
);