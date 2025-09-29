using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.AccountingCharts;

public class AccountingChartErrores
{
    public static readonly Error NoEncontrado = new
    (
        "AccountingChart.NoEncontrado",
        "No se encontro AccountingChart"
    );

    public static readonly Error Duplicado = new
    (
        "AccountingChart.Duplicado",
        "Ya existe una AccountingChart con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "AccountingChart.ErrorSave",
        "Error al guardar AccountingChart"
    );

    public static readonly Error ErrorEdit = new
    (
        "AccountingChart.ErrorEdit",
        "Error al editar AccountingChart"
    );

    public static readonly Error ErrorDelete = new
    (
        "AccountingChart.ErrorDelete",
        "Error al eliminar AccountingChart"
    );

    public static readonly Error ErrorConsulta = new
    (
        "AccountingChart.ErrorConsulta",
        "Error al listar AccountingChart"
    );
}
