using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpaceTypes;

public class SpaceTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SpaceType.NoEncontrado",
        "No se encontro SpaceType"
    );

    public static readonly Error Duplicado = new
    (
        "SpaceType.Duplicado",
        "Ya existe una SpaceType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SpaceType.ErrorSave",
        "Error al guardar SpaceType"
    );

    public static readonly Error ErrorEdit = new
    (
        "SpaceType.ErrorEdit",
        "Error al editar SpaceType"
    );

    public static readonly Error ErrorDelete = new
    (
        "SpaceType.ErrorDelete",
        "Error al eliminar SpaceType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SpaceType.ErrorConsulta",
        "Error al listar SpaceType"
    );
}
