using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.AccountTypes;

public class AccountTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "AccountType.NoEncontrado",
        "No se encontro AccountType"
    );

    public static readonly Error Duplicado = new
    (
        "AccountType.Duplicado",
        "Ya existe una AccountType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "AccountType.ErrorSave",
        "Error al guardar AccountType"
    );

    public static readonly Error ErrorEdit = new
    (
        "AccountType.ErrorEdit",
        "Error al editar AccountType"
    );

    public static readonly Error ErrorDelete = new
    (
        "AccountType.ErrorDelete",
        "Error al eliminar AccountType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "AccountType.ErrorConsulta",
        "Error al listar AccountType"
    );
}
