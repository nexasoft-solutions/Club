using NexaSoft.Agro.Domain.Abstractions;
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


    private User() { }

    private User(
        string? userApellidos,
        string? userNombres,
        string? nombreCompleto,
        string? userName,
        string? password,
        string? email,
        string? userDni,
        string? userTelefono,
        int estadoUser,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
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
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static User Create(
        string? userApellidos,
        string? userNombres,
        string? password,
        string? email,
        string? userDni,
        string? userTelefono,
        int estadoUser,
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var nombreCompleto = UserService.CreateNombreCompleto(userApellidos ?? "", userNombres ?? "");
        var userName = UserService.CreateUserName(userApellidos ?? "", userNombres ?? "");
        var entity = new User(
            userApellidos,
            userNombres,
            nombreCompleto,
            userName,
            password,
            email,
            userDni,
            userTelefono,
            estadoUser,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new UserCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? userApellidos,
        string? userNombres,
        string? password,
        string? email,
        string? userDni,
        string? userTelefono,
        DateTime utcNow,
        string? usuarioModificacion
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
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new UserUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoUser = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }


}
