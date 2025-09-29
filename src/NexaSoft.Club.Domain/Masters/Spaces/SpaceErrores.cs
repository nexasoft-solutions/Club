using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Spaces;

public class SpaceErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Space.NoEncontrado",
        "No se encontro Space"
    );

    public static readonly Error Duplicado = new
    (
        "Space.Duplicado",
        "Ya existe una Space con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Space.ErrorSave",
        "Error al guardar Space"
    );

    public static readonly Error ErrorEdit = new
    (
        "Space.ErrorEdit",
        "Error al editar Space"
    );

    public static readonly Error ErrorDelete = new
    (
        "Space.ErrorDelete",
        "Error al eliminar Space"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Space.ErrorConsulta",
        "Error al listar Space"
    );
}
