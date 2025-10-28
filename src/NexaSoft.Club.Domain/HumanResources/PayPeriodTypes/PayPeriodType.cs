using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

public class PayPeriodType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public int? Days { get; private set; }
    public string? Description { get; private set; }
    public int StatePayPeriodType { get; private set; }

    private PayPeriodType() { }

    private PayPeriodType(
        string? code, 
        string? name, 
        int? days, 
        string? description, 
        int statePayPeriodType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Days = days;
        Description = description;
        StatePayPeriodType = statePayPeriodType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayPeriodType Create(
        string? code, 
        string? name, 
        int? days, 
        string? description, 
        int statePayPeriodType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayPeriodType(
            code,
            name,
            days,
            description,
            statePayPeriodType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        int? days, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        Days = days;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayPeriodType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
