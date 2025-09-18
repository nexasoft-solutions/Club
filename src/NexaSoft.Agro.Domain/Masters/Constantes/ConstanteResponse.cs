namespace NexaSoft.Agro.Domain.Masters.Constantes;

public sealed record ConstanteResponse(
    long Id,
    string? TipoConstante,
    int Clave,
    string? Valor,
    int EstadoConstante,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
