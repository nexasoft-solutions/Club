using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.DocumentTypes;

public class DocumentTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "DocumentType.NoEncontrado",
        "No se encontro DocumentType"
    );

    public static readonly Error Duplicado = new
    (
        "DocumentType.Duplicado",
        "Ya existe una DocumentType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "DocumentType.ErrorSave",
        "Error al guardar DocumentType"
    );

    public static readonly Error ErrorEdit = new
    (
        "DocumentType.ErrorEdit",
        "Error al editar DocumentType"
    );

    public static readonly Error ErrorDelete = new
    (
        "DocumentType.ErrorDelete",
        "Error al eliminar DocumentType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "DocumentType.ErrorConsulta",
        "Error al listar DocumentType"
    );
}
