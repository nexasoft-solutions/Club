namespace NexaSoft.Agro.Api.Controllers.Masters.Menus.Request;

public sealed record CreateMenuRequest
(
    string? Label,
    string? Icon,
    string? Route,
    long? ParentId,
    string? UsuarioCreacion
);

