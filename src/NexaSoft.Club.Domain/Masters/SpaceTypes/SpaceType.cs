using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.SpaceTypes;

public class SpaceType : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StateSpaceType { get; private set; }

    private SpaceType() { }

    private SpaceType(
        string? name, 
        string? description, 
        int stateSpaceType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        StateSpaceType = stateSpaceType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static SpaceType Create(
        string? name, 
        string? description, 
        int stateSpaceType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new SpaceType(
            name,
            description,
            stateSpaceType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
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

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSpaceType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
