using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;

public class ConceptApplicationTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "ConceptApplicationType.NoEncontrado",
        "No se encontro ConceptApplicationType"
    );

    public static readonly Error Duplicado = new
    (
        "ConceptApplicationType.Duplicado",
        "Ya existe una ConceptApplicationType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "ConceptApplicationType.ErrorSave",
        "Error al guardar ConceptApplicationType"
    );

    public static readonly Error ErrorEdit = new
    (
        "ConceptApplicationType.ErrorEdit",
        "Error al editar ConceptApplicationType"
    );

    public static readonly Error ErrorDelete = new
    (
        "ConceptApplicationType.ErrorDelete",
        "Error al eliminar ConceptApplicationType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "ConceptApplicationType.ErrorConsulta",
        "Error al listar ConceptApplicationType"
    );
}
