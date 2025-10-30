using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.TaxRates;

public class TaxRateErrores
{
    public static readonly Error NoEncontrado = new
    (
        "TaxRate.NoEncontrado",
        "No se encontro TaxRate"
    );

    public static readonly Error Duplicado = new
    (
        "TaxRate.Duplicado",
        "Ya existe una TaxRate con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "TaxRate.ErrorSave",
        "Error al guardar TaxRate"
    );

    public static readonly Error ErrorEdit = new
    (
        "TaxRate.ErrorEdit",
        "Error al editar TaxRate"
    );

    public static readonly Error ErrorDelete = new
    (
        "TaxRate.ErrorDelete",
        "Error al eliminar TaxRate"
    );

    public static readonly Error ErrorConsulta = new
    (
        "TaxRate.ErrorConsulta",
        "Error al listar TaxRate"
    );
}
