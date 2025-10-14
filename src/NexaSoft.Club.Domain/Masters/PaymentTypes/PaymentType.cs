using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.PaymentTypes;

public class PaymentType : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StatePaymentType { get; private set; }

    private PaymentType() { }

    private PaymentType(
        string? name, 
        string? description, 
        int statePaymentType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        StatePaymentType = statePaymentType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PaymentType Create(
        string? name, 
        string? description, 
        int statePaymentType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PaymentType(
            name,
            description,
            statePaymentType,
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
        StatePaymentType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
