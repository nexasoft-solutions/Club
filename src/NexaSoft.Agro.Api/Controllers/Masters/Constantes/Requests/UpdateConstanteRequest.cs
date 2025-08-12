namespace NexaSoft.Agro.Api.Controllers.Masters.Constantes.Request;

public sealed record UpdateConstanteRequest(
    Guid Id,
    string? TipoConstante,
    string? Valor
);
