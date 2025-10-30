using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.LegalParameters;

public class LegalParameterErrores
{
    public static readonly Error NoEncontrado = new
    (
        "LegalParameter.NoEncontrado",
        "No se encontro LegalParameter"
    );

    public static readonly Error Duplicado = new
    (
        "LegalParameter.Duplicado",
        "Ya existe una LegalParameter con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "LegalParameter.ErrorSave",
        "Error al guardar LegalParameter"
    );

    public static readonly Error ErrorEdit = new
    (
        "LegalParameter.ErrorEdit",
        "Error al editar LegalParameter"
    );

    public static readonly Error ErrorDelete = new
    (
        "LegalParameter.ErrorDelete",
        "Error al eliminar LegalParameter"
    );

    public static readonly Error ErrorConsulta = new
    (
        "LegalParameter.ErrorConsulta",
        "Error al listar LegalParameter"
    );
}
