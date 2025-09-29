using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.MemberTypes;

public class MemberType : Entity
{
    public string? TypeName { get; private set; }
    public string? Description { get; private set; }
    //public decimal MonthlyFee { get; private set; }
    //public decimal EntryFee { get; private set; }
    public bool? HasFamilyDiscount { get; private set; }

    public decimal? FamilyDiscountRate { get; private set; }
    public int? MaxFamilyMembers { get; private set; }
    //public long? IncomeAccountId { get; private set; }
    //public AccountingChart? IncomeAccount { get; private set; }
    public int StateMemberType { get; private set; }

    private MemberType() { }

    private MemberType(
        string? typeName, 
        string? description, 
        //decimal monthlyFee, 
        //decimal entryFee, 
        bool? hasFamilyDiscount, 
        decimal? familyDiscountRate,
        int? maxFamilyMembers, 
        //long? incomeAccountId, 
        int stateMemberType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        TypeName = typeName;
        Description = description;
        //MonthlyFee = monthlyFee;
        //EntryFee = entryFee;
        HasFamilyDiscount = hasFamilyDiscount;
        FamilyDiscountRate = familyDiscountRate;
        MaxFamilyMembers = maxFamilyMembers;
        //IncomeAccountId = incomeAccountId;
        StateMemberType = stateMemberType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static MemberType Create(
        string? typeName, 
        string? description, 
        //decimal monthlyFee, 
        //decimal entryFee, 
        bool? hasFamilyDiscount, 
        decimal? familyDiscountRate,
        int? maxFamilyMembers, 
        //long? incomeAccountId, 
        int stateMemberType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new MemberType(
            typeName,
            description,
            //monthlyFee,
            //entryFee,
            hasFamilyDiscount,
            familyDiscountRate,
            maxFamilyMembers,
            //incomeAccountId,
            stateMemberType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? typeName, 
        string? description, 
        //decimal monthlyFee, 
        //decimal entryFee, 
        bool? hasFamilyDiscount, 
        decimal? familyDiscountRate,
        int? maxFamilyMembers, 
        //long? incomeAccountId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        TypeName = typeName;
        Description = description;
        //MonthlyFee = monthlyFee;
        //EntryFee = entryFee;
        HasFamilyDiscount = hasFamilyDiscount;
        FamilyDiscountRate = familyDiscountRate;
        MaxFamilyMembers = maxFamilyMembers;
        //IncomeAccountId = incomeAccountId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateMemberType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
