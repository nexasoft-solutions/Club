using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Users;

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
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
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
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static User Create(
        string? userApellidos,
        string? userNombres,
        string? password,
        string? email,
        string? userDni,
        string? userTelefono,
        int estadoUser,
        DateTime createdAt,
        string? createdBy
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
            createdAt,
            createdBy
        );
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
        string? updatedBy
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
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        EstadoUser = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }


}
