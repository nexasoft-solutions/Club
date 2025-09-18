using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Permissions;

public class PermissionErrores
{
    public static readonly Error NoEncontrado = new
        (
            "Permisiion.NoEncontrado",
            "No se encontro permiso"
        );

    public static readonly Error Duplicado = new
    (
        "Permisiion.Duplicado",
        "Ya existe un permiso con el mismo valor"
    );

    public static readonly Error ErrorSave = new
   (
       "Permisiion.ErrorSave",
       "Error al guardar permiso"
   );

    public static readonly Error ErrorEdit = new
    (
        "Permisiion.ErrorEdit",
        "Error al editar permiso"
    );

    public static readonly Error ErrorDelete = new
    (
        "Permisiion.ErrorDelete",
        "Error al eliminar permiso"
    );

    public static readonly Error PermisosNoEncontradosEnRol = new
    (
        "Permisiion.PermisosNoEncontradosEnRol",
        "No se encontraros los permisos"
    );

    public static Error PermisosNoEncontrados(IEnumerable<long> missingIds)
    {
        var idsString = string.Join(", ", missingIds.Select(id => id.ToString()));
        return new Error(
            "Permission.MultiplesNoEncontrados",
            $"Los siguientes permisos no existen: {idsString}"
        );
    }

    public static readonly Error ErrorConsulta = new
    (
        "Permission.ErrorConsulta",
        "Error al listar permisos"
    );

}
