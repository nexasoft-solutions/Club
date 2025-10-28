using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;

public class PayrollStatusTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollStatusType.NoEncontrado",
        "No se encontro PayrollStatusType"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollStatusType.Duplicado",
        "Ya existe una PayrollStatusType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollStatusType.ErrorSave",
        "Error al guardar PayrollStatusType"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollStatusType.ErrorEdit",
        "Error al editar PayrollStatusType"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollStatusType.ErrorDelete",
        "Error al eliminar PayrollStatusType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollStatusType.ErrorConsulta",
        "Error al listar PayrollStatusType"
    );
}
