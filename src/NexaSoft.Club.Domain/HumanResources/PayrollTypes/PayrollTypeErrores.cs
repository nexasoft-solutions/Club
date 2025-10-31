using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollTypes;

public class PayrollTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollType.NoEncontrado",
        "No se encontro PayrollType"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollType.Duplicado",
        "Ya existe una PayrollType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollType.ErrorSave",
        "Error al guardar PayrollType"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollType.ErrorEdit",
        "Error al editar PayrollType"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollType.ErrorDelete",
        "Error al eliminar PayrollType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollType.ErrorConsulta",
        "Error al listar PayrollType"
    );
}
