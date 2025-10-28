using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;

public class PaymentMethodType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StatePaymentMethodType { get; private set; }

    private PaymentMethodType() { }

    private PaymentMethodType(
        string? code, 
        string? name, 
        string? description, 
        int statePaymentMethodType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Description = description;
        StatePaymentMethodType = statePaymentMethodType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PaymentMethodType Create(
        string? code, 
        string? name, 
        string? description, 
        int statePaymentMethodType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PaymentMethodType(
            code,
            name,
            description,
            statePaymentMethodType,
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
        StatePaymentMethodType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
