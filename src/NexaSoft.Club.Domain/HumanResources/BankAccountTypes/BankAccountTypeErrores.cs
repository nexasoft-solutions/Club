using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.BankAccountTypes;

public class BankAccountTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "BankAccountType.NoEncontrado",
        "No se encontro BankAccountType"
    );

    public static readonly Error Duplicado = new
    (
        "BankAccountType.Duplicado",
        "Ya existe una BankAccountType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "BankAccountType.ErrorSave",
        "Error al guardar BankAccountType"
    );

    public static readonly Error ErrorEdit = new
    (
        "BankAccountType.ErrorEdit",
        "Error al editar BankAccountType"
    );

    public static readonly Error ErrorDelete = new
    (
        "BankAccountType.ErrorDelete",
        "Error al eliminar BankAccountType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "BankAccountType.ErrorConsulta",
        "Error al listar BankAccountType"
    );
}
