using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

public class PayPeriodTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayPeriodType.NoEncontrado",
        "No se encontro PayPeriodType"
    );

    public static readonly Error Duplicado = new
    (
        "PayPeriodType.Duplicado",
        "Ya existe una PayPeriodType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayPeriodType.ErrorSave",
        "Error al guardar PayPeriodType"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayPeriodType.ErrorEdit",
        "Error al editar PayPeriodType"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayPeriodType.ErrorDelete",
        "Error al eliminar PayPeriodType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayPeriodType.ErrorConsulta",
        "Error al listar PayPeriodType"
    );
}
