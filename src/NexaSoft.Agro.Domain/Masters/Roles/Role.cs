using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Roles;

public class Role : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    private Role() { }

    public Role(
        string? name,
        string? description,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)

    {
        Name = name;
        Description = description;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Role Create(
          string? name,
          string? description,
          DateTime fechaCreacion,
          string? usuarioCreacion
      )
    {
        var entity = new Role(
            name,
            description,
            fechaCreacion,
            usuarioCreacion
        );
        return entity;
    }

    public Result Update(
         string? name,
         string? description,
         DateTime utcNow,
         string? usuarioModificacion
     )
    {
        Name = name;
        Description = description;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;
        return Result.Success();
    }
}
