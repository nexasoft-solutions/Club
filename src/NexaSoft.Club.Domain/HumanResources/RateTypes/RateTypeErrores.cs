using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.RateTypes;

public class RateTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "RateType.NoEncontrado",
        "No se encontro RateType"
    );

    public static readonly Error Duplicado = new
    (
        "RateType.Duplicado",
        "Ya existe una RateType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "RateType.ErrorSave",
        "Error al guardar RateType"
    );

    public static readonly Error ErrorEdit = new
    (
        "RateType.ErrorEdit",
        "Error al editar RateType"
    );

    public static readonly Error ErrorDelete = new
    (
        "RateType.ErrorDelete",
        "Error al eliminar RateType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "RateType.ErrorConsulta",
        "Error al listar RateType"
    );
}
