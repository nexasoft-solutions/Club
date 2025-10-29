using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

public class PayrollFormulaErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollFormula.NoEncontrado",
        "No se encontro PayrollFormula"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollFormula.Duplicado",
        "Ya existe una PayrollFormula con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollFormula.ErrorSave",
        "Error al guardar PayrollFormula"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollFormula.ErrorEdit",
        "Error al editar PayrollFormula"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollFormula.ErrorDelete",
        "Error al eliminar PayrollFormula"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollFormula.ErrorConsulta",
        "Error al listar PayrollFormula"
    );
}
