using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Roles;

public class Role : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    private Role() { }

    public Role(
        Guid id,
        string? name,
        string? description,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        Name = name;
        Description = description;
        FechaCreacion = fechaCreacion;
    }

    public static Role Create(
          string? name,
          string? description,
          DateTime fechaCreacion
      )
    {
        var entity = new Role(
            Guid.NewGuid(),
            name,
            description,
            fechaCreacion
        );
        return entity;
    }

    public Result Update(
         string? name,
         string? description,
         DateTime utcNow
     )
    {
        Name = name;
        Description = description;
        FechaModificacion = utcNow;
        return Result.Success();
    }
}
