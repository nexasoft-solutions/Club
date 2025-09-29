using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Domain.Masters.Permissions;

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
        string? name,
        string? description,
        string? referenciaControl,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        ReferenciaControl = referenciaControl;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Permission Create(
        string? name,
        string? description,
        string? referenciaControl,
        DateTime fechaCreacion,
        string? createdAt
    )
    {
        var entity = new Permission(
            name,
            description,
            referenciaControl,
            fechaCreacion,
            createdAt
        );
        return entity;
    }

    public Result Update(
         string? name,
         string? description,
         string? referenciaControl,
         DateTime utcNow,
         string? updatedAt
     )
    {
        Name = name;
        Description = description;
        ReferenciaControl = referenciaControl;
        UpdatedAt = utcNow;
        UpdatedBy = UpdatedBy;
        return Result.Success();
    }

    // Métodos para manejar la relación con roles
    public void AddRole(long roleId)
    {
        if (!_rolePermissions.Any(rp => rp.RoleId == roleId))
        {
            _rolePermissions.Add(new RolePermission(roleId, Id));
        }
    }

    public void RemoveRole(long roleId)
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
