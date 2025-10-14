using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Ubigeos;

public class Ubigeo : Entity
{
    public string? Description { get; private set; }
    public int Level { get; private set; }
    public long? ParentId { get; private set; }
    public Ubigeo? Parent { get; private set; }
    public int StateUbigeo { get; private set; }

    private Ubigeo() { }

    private Ubigeo(
        string? description,
        int level,
        long? parentId,
        int stateUbigeo,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Description = description;
        Level = level;
        ParentId = parentId;
        StateUbigeo = stateUbigeo;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Ubigeo Create(
        string? description, 
        int level, 
        long? parentId,
        int stateUbigeo, 
        DateTime createdAt,
        string? createdBy
    )
    {
        var entity = new Ubigeo(
            description,
            level,
            parentId,
            stateUbigeo,
            createdAt,
            createdBy
        );
        //entity.RaiseDomainEvent(new UbigeoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? description,
        int level,
        long? parentId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        Description = description;
        Level = level;
        ParentId = parentId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;

        //RaiseDomainEvent(new UbigeoUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateUbigeo = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
