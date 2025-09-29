using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.AccountingEntries;

public class AccountingEntryErrores
{
    public static readonly Error NoEncontrado = new
    (
        "AccountingEntry.NoEncontrado",
        "No se encontro AccountingEntry"
    );

    public static readonly Error Duplicado = new
    (
        "AccountingEntry.Duplicado",
        "Ya existe una AccountingEntry con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "AccountingEntry.ErrorSave",
        "Error al guardar AccountingEntry"
    );

    public static readonly Error ErrorEdit = new
    (
        "AccountingEntry.ErrorEdit",
        "Error al editar AccountingEntry"
    );

    public static readonly Error ErrorDelete = new
    (
        "AccountingEntry.ErrorDelete",
        "Error al eliminar AccountingEntry"
    );

    public static readonly Error ErrorConsulta = new
    (
        "AccountingEntry.ErrorConsulta",
        "Error al listar AccountingEntry"
    );
}
