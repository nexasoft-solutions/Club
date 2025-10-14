using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Statuses;

public class StatusErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Status.NoEncontrado",
        "No se encontro Status"
    );

    public static readonly Error Duplicado = new
    (
        "Status.Duplicado",
        "Ya existe una Status con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Status.ErrorSave",
        "Error al guardar Status"
    );

    public static readonly Error ErrorEdit = new
    (
        "Status.ErrorEdit",
        "Error al editar Status"
    );

    public static readonly Error ErrorDelete = new
    (
        "Status.ErrorDelete",
        "Error al eliminar Status"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Status.ErrorConsulta",
        "Error al listar Status"
    );
}
