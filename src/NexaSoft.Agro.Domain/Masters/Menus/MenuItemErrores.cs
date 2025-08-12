
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Menus;

public class MenuItemErrores
{
    public static readonly Error NoEncontrado = new
      (
          "MenuItem.NoEncontrado",
          "No se encontro Menú"
      );

    public static readonly Error Duplicado = new
    (
        "MenuItem.Duplicado",
        "Ya existe un menú con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "MenuItem.ErrorSave",
        "Error al guardar el Menú"
    );

    public static readonly Error ErrorEdit = new
    (
        "MenuItem.ErrorEdit",
        "Error al editar el Menú"
    );

    public static readonly Error ErrorDelete = new
    (
        "MenuItem.ErrorDelete",
        "Error al eliminar Menu"
    );

    public static readonly Error ErrorConsulta = new
    (
        "MenuItem.ErrorConsulta",
        "Error al listar Menu"
    );

    public static readonly Error ErrorRolesNoAsignados = new
    (
        "MenuItem.ErrorRolesNoAsignados",
        "Error al asignar roles"
    );

    
}
