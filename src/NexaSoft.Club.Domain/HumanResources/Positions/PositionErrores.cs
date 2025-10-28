using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Positions;

public class PositionErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Position.NoEncontrado",
        "No se encontro Position"
    );

    public static readonly Error Duplicado = new
    (
        "Position.Duplicado",
        "Ya existe una Position con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Position.ErrorSave",
        "Error al guardar Position"
    );

    public static readonly Error ErrorEdit = new
    (
        "Position.ErrorEdit",
        "Error al editar Position"
    );

    public static readonly Error ErrorDelete = new
    (
        "Position.ErrorDelete",
        "Error al eliminar Position"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Position.ErrorConsulta",
        "Error al listar Position"
    );
}
