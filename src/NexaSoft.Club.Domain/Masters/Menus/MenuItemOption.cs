using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Menus;

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

    public int StateMenu { get; private set; } = (int)EstadosEnum.Activo;

    private MenuItemOption() { }

    private MenuItemOption(
        string? label,
        string? icon,
        string? route,
        long? parentId,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Label = label;
        Icon = icon;
        Route = route;
        ParentId = parentId;
        StateMenu = (int)EstadosEnum.Activo;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static MenuItemOption Create(
        string? label,
        string? icon,
        string? route,
        long? parentId,
        DateTime utcNow,
        string? createdAt
    )
    {
        return new MenuItemOption(
            label,
            icon,
            route,
            parentId,
            utcNow,
            createdAt
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
        string? updatedBy
    )
    {
        Label = label;
        Icon = icon;
        Route = route;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;
    }

    public void Delete(DateTime utcNow, string deletedBy)
    {
        StateMenu = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
    }

}
