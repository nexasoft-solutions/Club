using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

public class PayrollConceptDepartmentErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollConceptDepartment.NoEncontrado",
        "No se encontro PayrollConceptDepartment"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollConceptDepartment.Duplicado",
        "Ya existe una PayrollConceptDepartment con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollConceptDepartment.ErrorSave",
        "Error al guardar PayrollConceptDepartment"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollConceptDepartment.ErrorEdit",
        "Error al editar PayrollConceptDepartment"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollConceptDepartment.ErrorDelete",
        "Error al eliminar PayrollConceptDepartment"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollConceptDepartment.ErrorConsulta",
        "Error al listar PayrollConceptDepartment"
    );
}
