using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.ExpensesVouchers;

public class ExpenseVoucherErrores
{
    public static readonly Error NoEncontrado = new
    (
        "ExpenseVoucher.NoEncontrado",
        "No se encontro ExpenseVoucher"
    );

    public static readonly Error Duplicado = new
    (
        "ExpenseVoucher.Duplicado",
        "Ya existe una ExpenseVoucher con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "ExpenseVoucher.ErrorSave",
        "Error al guardar ExpenseVoucher"
    );

    public static readonly Error ErrorEdit = new
    (
        "ExpenseVoucher.ErrorEdit",
        "Error al editar ExpenseVoucher"
    );

    public static readonly Error ErrorDelete = new
    (
        "ExpenseVoucher.ErrorDelete",
        "Error al eliminar ExpenseVoucher"
    );

    public static readonly Error ErrorConsulta = new
    (
        "ExpenseVoucher.ErrorConsulta",
        "Error al listar ExpenseVoucher"
    );
}
