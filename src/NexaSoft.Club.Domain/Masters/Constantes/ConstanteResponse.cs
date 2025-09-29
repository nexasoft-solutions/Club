namespace NexaSoft.Club.Domain.Masters.Constantes;

public sealed record ConstanteResponse(
    long Id,
    string? TipoConstante,
    int Clave,
    string? Valor,
    int EstadoConstante,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
