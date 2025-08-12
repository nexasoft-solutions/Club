using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Users.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Users;

public class User : Entity
{
    public string? UserApellidos { get; private set; }
    public string? UserNombres { get; private set; }
    public string? NombreCompleto { get; private set; }
    public string? UserName { get; private set; }
    public string? Password { get; private set; }
    public string? Email { get; private set; }
    public string? UserDni { get; private set; }
    public string? UserTelefono { get; private set; }
    public int EstadoUser { get; private set; }

    //public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    /*private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();*/

    private User() { }

    private User(
        Guid id,
        string? userApellidos,
        string? userNombres,
        string? nombreCompleto,
        string? userName,
        string? password,
        string? email,
        string? userDni,
        string? userTelefono,
        int estadoUser,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        UserApellidos = userApellidos;
        UserNombres = userNombres;
        NombreCompleto = nombreCompleto;
        UserName = userName;
        Password = password;
        Email = email;
        UserDni = userDni;
        UserTelefono = userTelefono;
        EstadoUser = estadoUser;
        FechaCreacion = fechaCreacion;
    }

    public static User Create(
        string? userApellidos,
        string? userNombres,
        string? password,
        string? email,
        string? userDni,
        string? userTelefono,
        int estadoUser,
        DateTime fechaCreacion
    )
    {
        var nombreCompleto = UserService.CreateNombreCompleto(userApellidos ?? "", userNombres ?? "");
        var userName = UserService.CreateUserName(userApellidos ?? "", userNombres ?? "");
        var entity = new User(
            Guid.NewGuid(),
            userApellidos,
            userNombres,
            nombreCompleto,
            userName,
            password,
            email,
            userDni,
            userTelefono,
            estadoUser,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new UserCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
    Guid Id,
    string? userApellidos,
    string? userNombres,
    string? password,
    string? email,
    string? userDni,
    string? userTelefono,
    DateTime utcNow
)
    {
        UserApellidos = userApellidos;
        UserNombres = userNombres;

        // Actualiza NombreCompleto y UserName automáticamente
        NombreCompleto = UserService.CreateNombreCompleto(userApellidos ?? "", userNombres ?? "");
        UserName = UserService.CreateUserName(userApellidos ?? "", userNombres ?? "");

        Password = password;
        Email = email;
        UserDni = userDni;
        UserTelefono = userTelefono;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new UserUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoUser = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }

    /*public void AddRole(Guid roleId)
    {
        if (!_userRoles.Any(ur => ur.RoleId == roleId))
        {
            _userRoles.Add(new UserRole(Id, roleId));
        }
    }

    public bool RemoveRole(Guid roleId)
    {
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == roleId);
        if (userRole != null)
        {
            _userRoles.Remove(userRole);
            return true;
        }
        return false;
    }

    public void ClearRoles()
    {
        _userRoles.Clear();
    }*/

   

    // Para chequear si tiene un permiso
    /*public bool HasPermission(string permissionName)
    {
        return UserRoles.Any(ur =>
       ur.Role != null &&
       ur.Role.RolePermissions.Any(rp =>
           rp.Permission != null &&
           rp.Permission.Name == permissionName));
    }*/
}
