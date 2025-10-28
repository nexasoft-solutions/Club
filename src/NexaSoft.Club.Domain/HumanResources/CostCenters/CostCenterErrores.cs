using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CostCenters;

public class CostCenterErrores
{
    public static readonly Error NoEncontrado = new
    (
        "CostCenter.NoEncontrado",
        "No se encontro CostCenter"
    );

    public static readonly Error Duplicado = new
    (
        "CostCenter.Duplicado",
        "Ya existe una CostCenter con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "CostCenter.ErrorSave",
        "Error al guardar CostCenter"
    );

    public static readonly Error ErrorEdit = new
    (
        "CostCenter.ErrorEdit",
        "Error al editar CostCenter"
    );

    public static readonly Error ErrorDelete = new
    (
        "CostCenter.ErrorDelete",
        "Error al eliminar CostCenter"
    );

    public static readonly Error ErrorConsulta = new
    (
        "CostCenter.ErrorConsulta",
        "Error al listar CostCenter"
    );
}
