using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CostCenterTypes;

public class CostCenterTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "CostCenterType.NoEncontrado",
        "No se encontro CostCenterType"
    );

    public static readonly Error Duplicado = new
    (
        "CostCenterType.Duplicado",
        "Ya existe una CostCenterType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "CostCenterType.ErrorSave",
        "Error al guardar CostCenterType"
    );

    public static readonly Error ErrorEdit = new
    (
        "CostCenterType.ErrorEdit",
        "Error al editar CostCenterType"
    );

    public static readonly Error ErrorDelete = new
    (
        "CostCenterType.ErrorDelete",
        "Error al eliminar CostCenterType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "CostCenterType.ErrorConsulta",
        "Error al listar CostCenterType"
    );
}
