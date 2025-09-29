namespace NexaSoft.Club.Api.Controllers.Masters.Constantes.Request;

public sealed record CreateConstanteRequest(
    string? TipoConstante,
    string? Valor,
    string? UsuarioCreacion
);
