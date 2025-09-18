namespace NexaSoft.Agro.Api.Controllers.Masters.Constantes.Request;

public sealed record UpdateConstanteRequest(
    long Id,
    string? TipoConstante,
    string? Valor,
    string? UsuarioModificacion
);
