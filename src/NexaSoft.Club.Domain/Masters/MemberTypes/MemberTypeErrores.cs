using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.MemberTypes;

public class MemberTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "MemberType.NoEncontrado",
        "No se encontro MemberType"
    );

    public static readonly Error Duplicado = new
    (
        "MemberType.Duplicado",
        "Ya existe una MemberType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "MemberType.ErrorSave",
        "Error al guardar MemberType"
    );

    public static readonly Error ErrorEdit = new
    (
        "MemberType.ErrorEdit",
        "Error al editar MemberType"
    );

    public static readonly Error ErrorDelete = new
    (
        "MemberType.ErrorDelete",
        "Error al eliminar MemberType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "MemberType.ErrorConsulta",
        "Error al listar MemberType"
    );
}
