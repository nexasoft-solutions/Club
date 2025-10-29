using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.WorkSchedules;

public class WorkScheduleErrores
{
    public static readonly Error NoEncontrado = new
    (
        "WorkSchedule.NoEncontrado",
        "No se encontro WorkSchedule"
    );

    public static readonly Error Duplicado = new
    (
        "WorkSchedule.Duplicado",
        "Ya existe una WorkSchedule con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "WorkSchedule.ErrorSave",
        "Error al guardar WorkSchedule"
    );

    public static readonly Error ErrorEdit = new
    (
        "WorkSchedule.ErrorEdit",
        "Error al editar WorkSchedule"
    );

    public static readonly Error ErrorDelete = new
    (
        "WorkSchedule.ErrorDelete",
        "Error al eliminar WorkSchedule"
    );

    public static readonly Error ErrorConsulta = new
    (
        "WorkSchedule.ErrorConsulta",
        "Error al listar WorkSchedule"
    );
}
