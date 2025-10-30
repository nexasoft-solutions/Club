using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

public class PayrollConceptEmployeeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollConceptEmployee.NoEncontrado",
        "No se encontro PayrollConceptEmployee"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollConceptEmployee.Duplicado",
        "Ya existe una PayrollConceptEmployee con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollConceptEmployee.ErrorSave",
        "Error al guardar PayrollConceptEmployee"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollConceptEmployee.ErrorEdit",
        "Error al editar PayrollConceptEmployee"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollConceptEmployee.ErrorDelete",
        "Error al eliminar PayrollConceptEmployee"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollConceptEmployee.ErrorConsulta",
        "Error al listar PayrollConceptEmployee"
    );
}
