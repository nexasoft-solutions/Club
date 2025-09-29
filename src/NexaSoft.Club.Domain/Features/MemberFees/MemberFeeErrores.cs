using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.MemberFees;

public class MemberFeeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "MemberFee.NoEncontrado",
        "No se encontro MemberFee"
    );

    public static readonly Error Duplicado = new
    (
        "MemberFee.Duplicado",
        "Ya existe una MemberFee con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "MemberFee.ErrorSave",
        "Error al guardar MemberFee"
    );

    public static readonly Error ErrorEdit = new
    (
        "MemberFee.ErrorEdit",
        "Error al editar MemberFee"
    );

    public static readonly Error ErrorDelete = new
    (
        "MemberFee.ErrorDelete",
        "Error al eliminar MemberFee"
    );

    public static readonly Error ErrorConsulta = new
    (
        "MemberFee.ErrorConsulta",
        "Error al listar MemberFee"
    );
}
