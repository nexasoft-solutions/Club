using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

public class PayrollPeriodStatusErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollPeriodStatus.NoEncontrado",
        "No se encontro PayrollPeriodStatus"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollPeriodStatus.Duplicado",
        "Ya existe una PayrollPeriodStatus con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollPeriodStatus.ErrorSave",
        "Error al guardar PayrollPeriodStatus"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollPeriodStatus.ErrorEdit",
        "Error al editar PayrollPeriodStatus"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollPeriodStatus.ErrorDelete",
        "Error al eliminar PayrollPeriodStatus"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollPeriodStatus.ErrorConsulta",
        "Error al listar PayrollPeriodStatus"
    );
}
