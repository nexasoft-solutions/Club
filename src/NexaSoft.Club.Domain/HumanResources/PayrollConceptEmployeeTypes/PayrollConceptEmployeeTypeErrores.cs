using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

public class PayrollConceptEmployeeTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollConceptEmployeeType.NoEncontrado",
        "No se encontro PayrollConceptEmployeeType"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollConceptEmployeeType.Duplicado",
        "Ya existe una PayrollConceptEmployeeType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollConceptEmployeeType.ErrorSave",
        "Error al guardar PayrollConceptEmployeeType"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollConceptEmployeeType.ErrorEdit",
        "Error al editar PayrollConceptEmployeeType"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollConceptEmployeeType.ErrorDelete",
        "Error al eliminar PayrollConceptEmployeeType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollConceptEmployeeType.ErrorConsulta",
        "Error al listar PayrollConceptEmployeeType"
    );
}
