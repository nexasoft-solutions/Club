using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.FamilyMembers;

public class FamilyMemberErrores
{
    public static readonly Error NoEncontrado = new
    (
        "FamilyMember.NoEncontrado",
        "No se encontro FamilyMember"
    );

    public static readonly Error Duplicado = new
    (
        "FamilyMember.Duplicado",
        "Ya existe una FamilyMember con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "FamilyMember.ErrorSave",
        "Error al guardar FamilyMember"
    );

    public static readonly Error ErrorEdit = new
    (
        "FamilyMember.ErrorEdit",
        "Error al editar FamilyMember"
    );

    public static readonly Error ErrorDelete = new
    (
        "FamilyMember.ErrorDelete",
        "Error al eliminar FamilyMember"
    );

    public static readonly Error ErrorConsulta = new
    (
        "FamilyMember.ErrorConsulta",
        "Error al listar FamilyMember"
    );
}
