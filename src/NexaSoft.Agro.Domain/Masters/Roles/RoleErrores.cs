using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Roles;

public class RoleErrores
{
    public static readonly Error NoEncontrado = new
        (
            "Role.NoEncontrado",
            "No se encontro Rol"
        );

    public static readonly Error Duplicado = new
    (
        "Role.Duplicado",
        "Ya existe un rol con el mismo valor"
    );

    public static readonly Error ErrorSave = new
   (
       "Role.ErrorSave",
       "Error al guardar Rol"
   );

    public static readonly Error ErrorEdit = new
    (
        "Role.ErrorEdit",
        "Error al editar Rol"
    );

    public static readonly Error ErrorDelete = new
    (
        "Role.ErrorDelete",
        "Error al eliminar Rol"
    );
    public static readonly Error ErrorAsignarPermiso = new
    (
        "Role.ErrorAsignarPermiso",
        "Error al asignar permisos al rol."
    );

    public static readonly Error ErrorConsulta = new
    (
        "Role.ErrorConsulta",
        "Error al listar Roles"
    );

    public static readonly Error ErrorRolesNoEncontrados = new
    (
        "Role.ErrorRolesNoEncontrados",
        "No se encontraron roles para asignar al usuario."
    );

    public static readonly Error ErrorRevocarPermiso = new(
        "Role.ErrorRevocarPermiso",
        "Error al revocar permisos del rol"
    );

    public static readonly Error ErrorPermisosRol = new
    (
        "Role.ErrorPermisosRol",
        "Ninguno de los permisos especificados fue encontrado"
    );

    public static readonly Error ErrorLimpiarPermisos = new(
        "Role.ErrorLimpiarPermisos",
        "Error al limpiar todos los permisos del rol"
    );

    public static Error RolesNoEncontrados(IEnumerable<Guid> missingIds) => new(
       "Role.RolesNoEncontrados",
       $"Los siguientes roles no existen: {string.Join(", ", missingIds)}");

}
