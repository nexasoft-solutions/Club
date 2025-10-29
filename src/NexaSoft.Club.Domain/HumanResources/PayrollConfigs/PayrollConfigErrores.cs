using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

public class PayrollConfigErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollConfig.NoEncontrado",
        "No se encontro PayrollConfig"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollConfig.Duplicado",
        "Ya existe una PayrollConfig con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollConfig.ErrorSave",
        "Error al guardar PayrollConfig"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollConfig.ErrorEdit",
        "Error al editar PayrollConfig"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollConfig.ErrorDelete",
        "Error al eliminar PayrollConfig"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollConfig.ErrorConsulta",
        "Error al listar PayrollConfig"
    );
}
