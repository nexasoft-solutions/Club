using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Users;

public class UserErrores
{
    public static readonly Error NoEncontrado = new
    (
        "User.NoEncontrado",
        "No se encontro User"
    );

    public static readonly Error Duplicado = new
    (
        "User.Duplicado",
        "Ya existe una User con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "User.ErrorSave",
        "Error al guardar User"
    );

    public static readonly Error ErrorEdit = new
    (
        "User.ErrorEdit",
        "Error al editar User"
    );

    public static readonly Error ErrorDelete = new
    (
        "User.ErrorDelete",
        "Error al eliminar User"
    );

    public static readonly Error ErrorConsulta = new
    (
        "User.ErrorConsulta",
        "Error al listar User"
    );

    public static Error PasswordInvalido = new
    (
        "User.PasswordInvalido",
        "Usuario o password inválido!"
    );

    public static Error ErrorRolesAsignar = new
    (
        "User.ErrorRolesAsignar",
        "Error al asignar roles al Usuario"
    );

    public static Error ErrorRolesRevocar = new
    (
        "User.ErrorRolesRevocar",
        "Error al revocar roles al Usuario"
    );

    public static readonly Error ErrorAsignarRoles = new(
         "User.ErrorAsignarRoles",
         "Error al asignar roles al usuario"
     );

    public static readonly Error ErrorRevocarRoles = new(
        "User.ErrorRevocarRoles",
        "Error al revocar roles del usuario"
    );

    public static readonly Error ErrorLimpiarRoles = new(
        "User.ErrorLimpiarRoles",
        "Error al limpiar todos los roles del usuario"
    );

    public static readonly Error ErrorObtenerRolesPermisos = new(
        "User.ErrorObtenerRolesPermisos",
        "Error al obtener los roles y permisos del usuario"
    );

    public static readonly Error ErrorUserSinRolDefault = new(
        "User.ErrorUserSinRolDefault",
        "Usuario sin rol por defecto asignado"
    );
    public static readonly Error ErrorUserSinRol = new(
     "User.ErrorUserSinRol",
     "Usuario sin rol"
    );
}
