using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;

public class TimeRequestTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "TimeRequestType.NoEncontrado",
        "No se encontro TimeRequestType"
    );

    public static readonly Error Duplicado = new
    (
        "TimeRequestType.Duplicado",
        "Ya existe una TimeRequestType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "TimeRequestType.ErrorSave",
        "Error al guardar TimeRequestType"
    );

    public static readonly Error ErrorEdit = new
    (
        "TimeRequestType.ErrorEdit",
        "Error al editar TimeRequestType"
    );

    public static readonly Error ErrorDelete = new
    (
        "TimeRequestType.ErrorDelete",
        "Error al eliminar TimeRequestType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "TimeRequestType.ErrorConsulta",
        "Error al listar TimeRequestType"
    );
}
