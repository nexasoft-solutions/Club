using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.Currencies;

public class Currency : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public int StateCurrency { get; private set; }

    private Currency() { }

    private Currency(
        string? code, 
        string? name, 
        int stateCurrency, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        StateCurrency = stateCurrency;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Currency Create(
        string? code, 
        string? name, 
        int stateCurrency, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Currency(
            code,
            name,
            stateCurrency,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateCurrency = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
