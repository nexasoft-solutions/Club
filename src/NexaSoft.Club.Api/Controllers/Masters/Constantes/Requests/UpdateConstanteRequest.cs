namespace NexaSoft.Club.Api.Controllers.Masters.Constantes.Request;

public sealed record UpdateConstanteRequest(
    long Id,
    string? TipoConstante,
    string? Valor,
    string? UsuarioModificacion
);
