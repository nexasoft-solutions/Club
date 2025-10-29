using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.RateTypes;

namespace NexaSoft.Club.Domain.HumanResources.SpecialRates;

public class SpecialRate : Entity
{
    public long? RateTypeId { get; private set; }
    public RateType? RateType { get; private set; }
    public string? Name { get; private set; }
    public decimal Multiplier { get; private set; }
    public TimeOnly? StartTime { get; private set; }
    public TimeOnly? EndTime { get; private set; }
    public int StateSpecialRate { get; private set; }

    private SpecialRate() { }

    private SpecialRate(
        long? rateTypeId, 
        string? name, 
        decimal multiplier, 
        TimeOnly? startTime, 
        TimeOnly? endTime, 
        int stateSpecialRate, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        RateTypeId = rateTypeId;
        Name = name;
        Multiplier = multiplier;
        StartTime = startTime;
        EndTime = endTime;
        StateSpecialRate = stateSpecialRate;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static SpecialRate Create(
        long? rateTypeId, 
        string? name, 
        decimal multiplier, 
        TimeOnly? startTime, 
        TimeOnly? endTime, 
        int stateSpecialRate, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new SpecialRate(
            rateTypeId,
            name,
            multiplier,
            startTime,
            endTime,
            stateSpecialRate,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? rateTypeId, 
        string? name, 
        decimal multiplier, 
        TimeOnly? startTime, 
        TimeOnly? endTime, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        RateTypeId = rateTypeId;
        Name = name;
        Multiplier = multiplier;
        StartTime = startTime;
        EndTime = endTime;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSpecialRate = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
