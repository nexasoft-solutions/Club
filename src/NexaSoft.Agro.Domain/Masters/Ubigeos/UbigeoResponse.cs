
namespace NexaSoft.Agro.Domain.Masters.Ubigeos;

public sealed record UbigeoResponse(
    Guid Id,
    string? Descripcion,
    string Nivel,
    Guid? PadreId,
    string? DescripcionPadre,
    DateTime FechaCreacion
);
