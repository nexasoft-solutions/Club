using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.UserTypes;

public class UserTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "UserType.NoEncontrado",
        "No se encontro UserType"
    );

    public static readonly Error Duplicado = new
    (
        "UserType.Duplicado",
        "Ya existe una UserType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "UserType.ErrorSave",
        "Error al guardar UserType"
    );

    public static readonly Error ErrorEdit = new
    (
        "UserType.ErrorEdit",
        "Error al editar UserType"
    );

    public static readonly Error ErrorDelete = new
    (
        "UserType.ErrorDelete",
        "Error al eliminar UserType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "UserType.ErrorConsulta",
        "Error al listar UserType"
    );
}
