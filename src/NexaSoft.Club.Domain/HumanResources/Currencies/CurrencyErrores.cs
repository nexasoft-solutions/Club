using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Currencies;

public class CurrencyErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Currency.NoEncontrado",
        "No se encontro Currency"
    );

    public static readonly Error Duplicado = new
    (
        "Currency.Duplicado",
        "Ya existe una Currency con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Currency.ErrorSave",
        "Error al guardar Currency"
    );

    public static readonly Error ErrorEdit = new
    (
        "Currency.ErrorEdit",
        "Error al editar Currency"
    );

    public static readonly Error ErrorDelete = new
    (
        "Currency.ErrorDelete",
        "Error al eliminar Currency"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Currency.ErrorConsulta",
        "Error al listar Currency"
    );
}
