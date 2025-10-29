using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public class PayrollPeriodErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollPeriod.NoEncontrado",
        "No se encontro PayrollPeriod"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollPeriod.Duplicado",
        "Ya existe una PayrollPeriod con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollPeriod.ErrorSave",
        "Error al guardar PayrollPeriod"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollPeriod.ErrorEdit",
        "Error al editar PayrollPeriod"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollPeriod.ErrorDelete",
        "Error al eliminar PayrollPeriod"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollPeriod.ErrorConsulta",
        "Error al listar PayrollPeriod"
    );
}
