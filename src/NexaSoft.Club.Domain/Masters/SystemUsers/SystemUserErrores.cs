using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SystemUsers;

public class SystemUserErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SystemUser.NoEncontrado",
        "No se encontro SystemUser"
    );

    public static readonly Error Duplicado = new
    (
        "SystemUser.Duplicado",
        "Ya existe una SystemUser con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SystemUser.ErrorSave",
        "Error al guardar SystemUser"
    );

    public static readonly Error ErrorEdit = new
    (
        "SystemUser.ErrorEdit",
        "Error al editar SystemUser"
    );

    public static readonly Error ErrorDelete = new
    (
        "SystemUser.ErrorDelete",
        "Error al eliminar SystemUser"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SystemUser.ErrorConsulta",
        "Error al listar SystemUser"
    );
}
