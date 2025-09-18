
namespace NexaSoft.Agro.Domain.Masters.Ubigeos;

public sealed record UbigeoResponse(
    long Id,
    string? Descripcion,
    string Nivel,
    long? PadreId,
    string? DescripcionPadre,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
