using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Menus;

public class MenuItemOption : Entity
{
    public string? Label { get; private set; }
    public string? Icon { get; private set; }
    public string? Route { get; private set; }

    public Guid? ParentId { get; private set; }
    public MenuItemOption? Parent { get; private set; }


    //public List<MenuItemOption> Children { get; set; } = new();
    public ICollection<MenuItemOption> Children { get; private set; } = new List<MenuItemOption>();

    public ICollection<MenuRole> Roles { get; private set; } = new List<MenuRole>();

    public int EstadoMenu { get; private set; } = (int)EstadosEnum.Activo;

    private MenuItemOption() { }

    private MenuItemOption(
        Guid id,
        string? label,
        string? icon,
        string? route,
        Guid? parentId,
        DateTime fechaCreacion
    ): base(id, fechaCreacion)
    {
        Label = label;
        Icon = icon;
        Route = route;
        ParentId = parentId;
        EstadoMenu = (int)EstadosEnum.Activo;
    }

    public static MenuItemOption Create(
        string? label,
        string? icon,
        string? route,
        Guid? parentId,
        DateTime utcNow
    ){
        return new MenuItemOption(
            Guid.NewGuid(),
            label,
            icon,
            route,
            parentId,
            utcNow
        );
    }

    public void AddRole(Guid roleId)
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
        DateTime utcNow
    ){
        Label = label;
        Icon = icon;
        Route = route;
        FechaModificacion = utcNow;
    }

    public void Delete(DateTime utcNow)
    {
        EstadoMenu = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
    }

}
