using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.SpecialRates;

public class SpecialRateErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SpecialRate.NoEncontrado",
        "No se encontro SpecialRate"
    );

    public static readonly Error Duplicado = new
    (
        "SpecialRate.Duplicado",
        "Ya existe una SpecialRate con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SpecialRate.ErrorSave",
        "Error al guardar SpecialRate"
    );

    public static readonly Error ErrorEdit = new
    (
        "SpecialRate.ErrorEdit",
        "Error al editar SpecialRate"
    );

    public static readonly Error ErrorDelete = new
    (
        "SpecialRate.ErrorDelete",
        "Error al eliminar SpecialRate"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SpecialRate.ErrorConsulta",
        "Error al listar SpecialRate"
    );
}
