using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members;

public class MemberErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Member.NoEncontrado",
        "No se encontro Member"
    );

    public static readonly Error Duplicado = new
    (
        "Member.Duplicado",
        "Ya existe una Member con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Member.ErrorSave",
        "Error al guardar Member"
    );

    public static readonly Error ErrorEdit = new
    (
        "Member.ErrorEdit",
        "Error al editar Member"
    );

    public static readonly Error ErrorDelete = new
    (
        "Member.ErrorDelete",
        "Error al eliminar Member"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Member.ErrorConsulta",
        "Error al listar Member"
    );

    public static readonly Error TipoNoExiste = new
    (
        "Member.TipoNoExiste",
        "El tipo de menbresia no existe"
    );

    
}
