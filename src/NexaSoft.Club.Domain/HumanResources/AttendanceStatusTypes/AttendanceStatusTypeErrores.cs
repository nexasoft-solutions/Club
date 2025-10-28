using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

public class AttendanceStatusTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "AttendanceStatusType.NoEncontrado",
        "No se encontro AttendanceStatusType"
    );

    public static readonly Error Duplicado = new
    (
        "AttendanceStatusType.Duplicado",
        "Ya existe una AttendanceStatusType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "AttendanceStatusType.ErrorSave",
        "Error al guardar AttendanceStatusType"
    );

    public static readonly Error ErrorEdit = new
    (
        "AttendanceStatusType.ErrorEdit",
        "Error al editar AttendanceStatusType"
    );

    public static readonly Error ErrorDelete = new
    (
        "AttendanceStatusType.ErrorDelete",
        "Error al eliminar AttendanceStatusType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "AttendanceStatusType.ErrorConsulta",
        "Error al listar AttendanceStatusType"
    );
}
