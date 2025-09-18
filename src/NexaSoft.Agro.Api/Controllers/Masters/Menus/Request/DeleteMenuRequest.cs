namespace NexaSoft.Agro.Api.Controllers.Masters.Menus.Request;

public sealed record DeleteMenuRequest(
    long Id,
    string UsuarioEliminacion 
);

