using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;

public class ConceptCalculationTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "ConceptCalculationType.NoEncontrado",
        "No se encontro ConceptCalculationType"
    );

    public static readonly Error Duplicado = new
    (
        "ConceptCalculationType.Duplicado",
        "Ya existe una ConceptCalculationType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "ConceptCalculationType.ErrorSave",
        "Error al guardar ConceptCalculationType"
    );

    public static readonly Error ErrorEdit = new
    (
        "ConceptCalculationType.ErrorEdit",
        "Error al editar ConceptCalculationType"
    );

    public static readonly Error ErrorDelete = new
    (
        "ConceptCalculationType.ErrorDelete",
        "Error al eliminar ConceptCalculationType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "ConceptCalculationType.ErrorConsulta",
        "Error al listar ConceptCalculationType"
    );
}
