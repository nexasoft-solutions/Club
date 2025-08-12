namespace NexaSoft.Agro.Domain.Masters.Constantes;

public sealed record ConstanteResponse(
    Guid Id,
    string? TipoConstante,
    int Clave,
    string? Valor,
    int EstadoConstante,
    DateTime FechaCreacion
);
