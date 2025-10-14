using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

public class SpaceAvailabilityErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SpaceAvailability.NoEncontrado",
        "No se encontro SpaceAvailability"
    );

    public static readonly Error Duplicado = new
    (
        "SpaceAvailability.Duplicado",
        "Ya existe una SpaceAvailability con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SpaceAvailability.ErrorSave",
        "Error al guardar SpaceAvailability"
    );

    public static readonly Error ErrorEdit = new
    (
        "SpaceAvailability.ErrorEdit",
        "Error al editar SpaceAvailability"
    );

    public static readonly Error ErrorDelete = new
    (
        "SpaceAvailability.ErrorDelete",
        "Error al eliminar SpaceAvailability"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SpaceAvailability.ErrorConsulta",
        "Error al listar SpaceAvailability"
    );
}
