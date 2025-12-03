using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Domain.Masters.Roles;

public class Role : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    private Role() { }

    public ICollection<MenuRole> MenuRoles { get; private set; } = new List<MenuRole>();


    public Role(
        string? name,
        string? description,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Role Create(
          string? name,
          string? description,
          DateTime createdAt,
          string? createdBy
      )
    {
        var entity = new Role(
            name,
            description,
            createdAt,
            createdBy
        );
        return entity;
    }

    public Result Update(
         string? name,
         string? description,
         DateTime utcNow,
         string? updatedBy
     )
    {
        Name = name;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;
        return Result.Success();
    }
}
