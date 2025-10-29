using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Expenses;

public class ExpenseErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Expense.NoEncontrado",
        "No se encontro Expense"
    );

    public static readonly Error Duplicado = new
    (
        "Expense.Duplicado",
        "Ya existe una Expense con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Expense.ErrorSave",
        "Error al guardar Expense"
    );

    public static readonly Error ErrorEdit = new
    (
        "Expense.ErrorEdit",
        "Error al editar Expense"
    );

    public static readonly Error ErrorDelete = new
    (
        "Expense.ErrorDelete",
        "Error al eliminar Expense"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Expense.ErrorConsulta",
        "Error al listar Expense"
    );
}
