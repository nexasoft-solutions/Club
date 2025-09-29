using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Periodicities;

public class Periodicity : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StatePeriodicity { get; private set; }

    private Periodicity() { }

    private Periodicity(
        string? name, 
        string? description, 
        int statePeriodicity, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        StatePeriodicity = statePeriodicity;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Periodicity Create(
        string? name, 
        string? description, 
        int statePeriodicity, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Periodicity(
            name,
            description,
            statePeriodicity,
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
        StatePeriodicity = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
