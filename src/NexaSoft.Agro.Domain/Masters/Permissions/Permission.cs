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
        string? name,
        string? description,
        string? referenciaControl,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        Name = name;
        Description = description;
        ReferenciaControl = referenciaControl;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Permission Create(
        string? name,
        string? description,
        string? referenciaControl,
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Permission(
            name,
            description,
            referenciaControl,
            fechaCreacion,
            usuarioCreacion
        );
        return entity;
    }

    public Result Update(
         string? name,
         string? description,
         string? referenciaControl,
         DateTime utcNow,
         string? usuarioModificacion
     )
    {
        Name = name;
        Description = description;
        ReferenciaControl = referenciaControl;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;
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
