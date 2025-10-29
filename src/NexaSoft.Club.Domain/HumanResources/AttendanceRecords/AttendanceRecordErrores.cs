using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

public class AttendanceRecordErrores
{
    public static readonly Error NoEncontrado = new
    (
        "AttendanceRecord.NoEncontrado",
        "No se encontro AttendanceRecord"
    );

    public static readonly Error Duplicado = new
    (
        "AttendanceRecord.Duplicado",
        "Ya existe una AttendanceRecord con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "AttendanceRecord.ErrorSave",
        "Error al guardar AttendanceRecord"
    );

    public static readonly Error ErrorEdit = new
    (
        "AttendanceRecord.ErrorEdit",
        "Error al editar AttendanceRecord"
    );

    public static readonly Error ErrorDelete = new
    (
        "AttendanceRecord.ErrorDelete",
        "Error al eliminar AttendanceRecord"
    );

    public static readonly Error ErrorConsulta = new
    (
        "AttendanceRecord.ErrorConsulta",
        "Error al listar AttendanceRecord"
    );
}
