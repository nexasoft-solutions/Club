using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Banks;

public class BankErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Bank.NoEncontrado",
        "No se encontro Bank"
    );

    public static readonly Error Duplicado = new
    (
        "Bank.Duplicado",
        "Ya existe una Bank con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Bank.ErrorSave",
        "Error al guardar Bank"
    );

    public static readonly Error ErrorEdit = new
    (
        "Bank.ErrorEdit",
        "Error al editar Bank"
    );

    public static readonly Error ErrorDelete = new
    (
        "Bank.ErrorDelete",
        "Error al eliminar Bank"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Bank.ErrorConsulta",
        "Error al listar Bank"
    );
}
