
namespace NexaSoft.Club.Domain.Masters.Ubigeos;

public sealed record UbigeoResponse(
    long Id,
    string? Descripcion,
    string Nivel,
    long? PadreId,
    string? DescripcionPadre,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
