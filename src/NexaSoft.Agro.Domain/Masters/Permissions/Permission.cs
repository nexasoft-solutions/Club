using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Domain.Masters.Permissions;

public class Permission : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? ReferenciaControl { get; private set; }

    // Campo privado para la relación (nomenclatura consistente)
    private readonly List<RolePermission> _rolePermissions = new();

    // Propiedad de solo lectura
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    private Permission() { }
    public Permission(
        Guid id,
        string? name,
        string? description,
        string? referenciaControl,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        Name = name;
        Description = description;
        ReferenciaControl = referenciaControl;
        FechaCreacion = fechaCreacion;
    }

    public static Permission Create(
        string? name,
        string? description,
        string? referenciaControl,
        DateTime fechaCreacion
    )
    {
        var entity = new Permission(
            Guid.NewGuid(),
            name,
            description,
            referenciaControl,
            fechaCreacion
        );
        return entity;
    }

    public Result Update(
         string? name,
         string? description,
         string? referenciaControl,
         DateTime utcNow
     )
    {
        Name = name;
        Description = description;
        ReferenciaControl = referenciaControl;
        FechaModificacion = utcNow;
        return Result.Success();
    }

    // Métodos para manejar la relación con roles
    public void AddRole(Guid roleId)
    {
        if (!_rolePermissions.Any(rp => rp.RoleId == roleId))
        {
            _rolePermissions.Add(new RolePermission(roleId, Id));
        }
    }

    public void RemoveRole(Guid roleId)
    {
        var rp = _rolePermissions.FirstOrDefault(rp => rp.RoleId == roleId);
        if (rp != null)
        {
            _rolePermissions.Remove(rp);
        }
    }

    public void ClearRoles()
    {
        _rolePermissions.Clear();
    }

}
