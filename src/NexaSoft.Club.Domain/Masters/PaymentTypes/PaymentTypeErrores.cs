using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.PaymentTypes;

public class PaymentTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PaymentType.NoEncontrado",
        "No se encontro PaymentType"
    );

    public static readonly Error Duplicado = new
    (
        "PaymentType.Duplicado",
        "Ya existe una PaymentType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PaymentType.ErrorSave",
        "Error al guardar PaymentType"
    );

    public static readonly Error ErrorEdit = new
    (
        "PaymentType.ErrorEdit",
        "Error al editar PaymentType"
    );

    public static readonly Error ErrorDelete = new
    (
        "PaymentType.ErrorDelete",
        "Error al eliminar PaymentType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PaymentType.ErrorConsulta",
        "Error al listar PaymentType"
    );
}
