using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;

public class PaymentMethodTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PaymentMethodType.NoEncontrado",
        "No se encontro PaymentMethodType"
    );

    public static readonly Error Duplicado = new
    (
        "PaymentMethodType.Duplicado",
        "Ya existe una PaymentMethodType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PaymentMethodType.ErrorSave",
        "Error al guardar PaymentMethodType"
    );

    public static readonly Error ErrorEdit = new
    (
        "PaymentMethodType.ErrorEdit",
        "Error al editar PaymentMethodType"
    );

    public static readonly Error ErrorDelete = new
    (
        "PaymentMethodType.ErrorDelete",
        "Error al eliminar PaymentMethodType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PaymentMethodType.ErrorConsulta",
        "Error al listar PaymentMethodType"
    );
}
