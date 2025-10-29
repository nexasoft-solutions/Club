using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.RateTypes;

public class RateType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StateRateType { get; private set; }

    private RateType() { }

    private RateType(
        string? code, 
        string? name, 
        string? description, 
        int stateRateType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Description = description;
        StateRateType = stateRateType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static RateType Create(
        string? code, 
        string? name, 
        string? description, 
        int stateRateType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new RateType(
            code,
            name,
            description,
            stateRateType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateRateType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
