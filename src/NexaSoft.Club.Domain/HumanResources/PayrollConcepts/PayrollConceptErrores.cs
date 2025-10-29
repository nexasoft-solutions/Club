using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

public class PayrollConceptErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollConcept.NoEncontrado",
        "No se encontro PayrollConcept"
    );

    public static readonly Error Duplicado = new
    (
        "PayrollConcept.Duplicado",
        "Ya existe una PayrollConcept con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "PayrollConcept.ErrorSave",
        "Error al guardar PayrollConcept"
    );

    public static readonly Error ErrorEdit = new
    (
        "PayrollConcept.ErrorEdit",
        "Error al editar PayrollConcept"
    );

    public static readonly Error ErrorDelete = new
    (
        "PayrollConcept.ErrorDelete",
        "Error al eliminar PayrollConcept"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollConcept.ErrorConsulta",
        "Error al listar PayrollConcept"
    );
}
