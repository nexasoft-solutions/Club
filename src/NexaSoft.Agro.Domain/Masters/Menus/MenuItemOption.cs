using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Menus;

public class MenuItemOption : Entity
{
    public string? Label { get; private set; }
    public string? Icon { get; private set; }
    public string? Route { get; private set; }

    public long? ParentId { get; private set; }
    public MenuItemOption? Parent { get; private set; }


    //public List<MenuItemOption> Children { get; set; } = new();
    public ICollection<MenuItemOption> Children { get; private set; } = new List<MenuItemOption>();

    public ICollection<MenuRole> Roles { get; private set; } = new List<MenuRole>();

    public int EstadoMenu { get; private set; } = (int)EstadosEnum.Activo;

    private MenuItemOption() { }

    private MenuItemOption(
        string? label,
        string? icon,
        string? route,
        long? parentId,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        Label = label;
        Icon = icon;
        Route = route;
        ParentId = parentId;
        EstadoMenu = (int)EstadosEnum.Activo;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static MenuItemOption Create(
        string? label,
        string? icon,
        string? route,
        long? parentId,
        DateTime utcNow,
        string? usuarioCreacion
    ){
        return new MenuItemOption(
            label,
            icon,
            route,
            parentId,
            utcNow,
            usuarioCreacion
        );
    }

    public void AddRole(long roleId)
    {
        if (!Roles.Any(r => r.RoleId == roleId))
        {
            Roles.Add(new MenuRole(Id, roleId));
        }
    }

    public void Update(
        string? label,
        string? icon,
        string? route,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        Label = label;
        Icon = icon;
        Route = route;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;
    }

    public void Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoMenu = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
    }

}
