using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.TimeRequests;

public class TimeRequestErrores
{
    public static readonly Error NoEncontrado = new
    (
        "TimeRequest.NoEncontrado",
        "No se encontro TimeRequest"
    );

    public static readonly Error Duplicado = new
    (
        "TimeRequest.Duplicado",
        "Ya existe una TimeRequest con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "TimeRequest.ErrorSave",
        "Error al guardar TimeRequest"
    );

    public static readonly Error ErrorEdit = new
    (
        "TimeRequest.ErrorEdit",
        "Error al editar TimeRequest"
    );

    public static readonly Error ErrorDelete = new
    (
        "TimeRequest.ErrorDelete",
        "Error al eliminar TimeRequest"
    );

    public static readonly Error ErrorConsulta = new
    (
        "TimeRequest.ErrorConsulta",
        "Error al listar TimeRequest"
    );
}
