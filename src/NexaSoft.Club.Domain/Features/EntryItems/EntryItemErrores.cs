using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.EntryItems;

public class EntryItemErrores
{
    public static readonly Error NoEncontrado = new
    (
        "EntryItem.NoEncontrado",
        "No se encontro EntryItem"
    );

    public static readonly Error Duplicado = new
    (
        "EntryItem.Duplicado",
        "Ya existe una EntryItem con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "EntryItem.ErrorSave",
        "Error al guardar EntryItem"
    );

    public static readonly Error ErrorEdit = new
    (
        "EntryItem.ErrorEdit",
        "Error al editar EntryItem"
    );

    public static readonly Error ErrorDelete = new
    (
        "EntryItem.ErrorDelete",
        "Error al eliminar EntryItem"
    );

    public static readonly Error ErrorConsulta = new
    (
        "EntryItem.ErrorConsulta",
        "Error al listar EntryItem"
    );
}
