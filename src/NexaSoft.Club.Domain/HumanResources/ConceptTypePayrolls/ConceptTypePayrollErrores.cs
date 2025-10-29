using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;

public class ConceptTypePayrollErrores
{
    public static readonly Error NoEncontrado = new
    (
        "ConceptTypePayroll.NoEncontrado",
        "No se encontro ConceptTypePayroll"
    );

    public static readonly Error Duplicado = new
    (
        "ConceptTypePayroll.Duplicado",
        "Ya existe una ConceptTypePayroll con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "ConceptTypePayroll.ErrorSave",
        "Error al guardar ConceptTypePayroll"
    );

    public static readonly Error ErrorEdit = new
    (
        "ConceptTypePayroll.ErrorEdit",
        "Error al editar ConceptTypePayroll"
    );

    public static readonly Error ErrorDelete = new
    (
        "ConceptTypePayroll.ErrorDelete",
        "Error al eliminar ConceptTypePayroll"
    );

    public static readonly Error ErrorConsulta = new
    (
        "ConceptTypePayroll.ErrorConsulta",
        "Error al listar ConceptTypePayroll"
    );
}
