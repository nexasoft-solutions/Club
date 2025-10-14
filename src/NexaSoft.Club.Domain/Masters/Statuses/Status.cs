using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Statuses;

public class Status : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StateStatus { get; private set; }

    private Status() { }

    private Status(
        string? name, 
        string? description, 
        int stateStatus, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        StateStatus = stateStatus;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Status Create(
        string? name, 
        string? description, 
        int stateStatus, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Status(
            name,
            description,
            stateStatus,
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
        StateStatus = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
