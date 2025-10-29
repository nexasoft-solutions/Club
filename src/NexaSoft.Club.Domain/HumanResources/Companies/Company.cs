using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.Companies;

public class Company : Entity
{
    public string? Ruc { get; private set; }
    public string? BusinessName { get; private set; }
    public string? TradeName { get; private set; }
    public string? Address { get; private set; }
    public string? Phone { get; private set; }
    public string? Website { get; private set; }
    public int StateCompany { get; private set; }

    private Company() { }

    private Company(
        string? ruc, 
        string? businessName, 
        string? tradeName, 
        string? address, 
        string? phone, 
        string? website, 
        int stateCompany, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Ruc = ruc;
        BusinessName = businessName;
        TradeName = tradeName;
        Address = address;
        Phone = phone;
        Website = website;
        StateCompany = stateCompany;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Company Create(
        string? ruc, 
        string? businessName, 
        string? tradeName, 
        string? address, 
        string? phone, 
        string? website, 
        int stateCompany, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Company(
            ruc,
            businessName,
            tradeName,
            address,
            phone,
            website,
            stateCompany,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? ruc, 
        string? businessName, 
        string? tradeName, 
        string? address, 
        string? phone, 
        string? website, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Ruc = ruc;
        BusinessName = businessName;
        TradeName = tradeName;
        Address = address;
        Phone = phone;
        Website = website;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateCompany = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
