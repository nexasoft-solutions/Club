using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

public class CompanyBankAccountErrores
{
    public static readonly Error NoEncontrado = new
    (
        "CompanyBankAccount.NoEncontrado",
        "No se encontro CompanyBankAccount"
    );

    public static readonly Error Duplicado = new
    (
        "CompanyBankAccount.Duplicado",
        "Ya existe una CompanyBankAccount con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "CompanyBankAccount.ErrorSave",
        "Error al guardar CompanyBankAccount"
    );

    public static readonly Error ErrorEdit = new
    (
        "CompanyBankAccount.ErrorEdit",
        "Error al editar CompanyBankAccount"
    );

    public static readonly Error ErrorDelete = new
    (
        "CompanyBankAccount.ErrorDelete",
        "Error al eliminar CompanyBankAccount"
    );

    public static readonly Error ErrorConsulta = new
    (
        "CompanyBankAccount.ErrorConsulta",
        "Error al listar CompanyBankAccount"
    );
}
