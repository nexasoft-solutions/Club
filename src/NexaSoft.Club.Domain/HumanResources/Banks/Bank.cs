using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.Banks;

public class Bank : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? WebSite { get; private set; }
    public string? Phone { get; private set; }
    public int StateBank { get; private set; }

    private Bank() { }

    private Bank(
        string? code, 
        string? name, 
        string? webSite, 
        string? phone, 
        int stateBank, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        WebSite = webSite;
        Phone = phone;
        StateBank = stateBank;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Bank Create(
        string? code, 
        string? name, 
        string? webSite, 
        string? phone, 
        int stateBank, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Bank(
            code,
            name,
            webSite,
            phone,
            stateBank,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        string? webSite, 
        string? phone, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        WebSite = webSite;
        Phone = phone;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateBank = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
