using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.MemberStatuses;

public class MemberStatusErrores
{
    public static readonly Error NoEncontrado = new
    (
        "MemberStatus.NoEncontrado",
        "No se encontro MemberStatus"
    );

    public static readonly Error Duplicado = new
    (
        "MemberStatus.Duplicado",
        "Ya existe una MemberStatus con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "MemberStatus.ErrorSave",
        "Error al guardar MemberStatus"
    );

    public static readonly Error ErrorEdit = new
    (
        "MemberStatus.ErrorEdit",
        "Error al editar MemberStatus"
    );

    public static readonly Error ErrorDelete = new
    (
        "MemberStatus.ErrorDelete",
        "Error al eliminar MemberStatus"
    );

    public static readonly Error ErrorConsulta = new
    (
        "MemberStatus.ErrorConsulta",
        "Error al listar MemberStatus"
    );
}
