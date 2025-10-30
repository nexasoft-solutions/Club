using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.TaxRates;

public class TaxRate : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public decimal RateValue { get; private set; }
    public string? RateType { get; private set; }
    public decimal? MinAmount { get; private set; }
    public decimal? MaxAmount { get; private set; }
    public DateOnly EffectiveDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public string? Category { get; private set; }
    public string? Description { get; private set; }
    public string? AppliesTo { get; private set; }
    public int StateTaxRate { get; private set; }

    private TaxRate() { }

    private TaxRate(
        string? code, 
        string? name, 
        decimal rateValue, 
        string? rateType, 
        decimal? minAmount, 
        decimal? maxAmount, 
        DateOnly effectiveDate, 
        DateOnly? endDate, 
        string? category, 
        string? description, 
        string? appliesTo, 
        int stateTaxRate, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        RateValue = rateValue;
        RateType = rateType;
        MinAmount = minAmount;
        MaxAmount = maxAmount;
        EffectiveDate = effectiveDate;
        EndDate = endDate;
        Category = category;
        Description = description;
        AppliesTo = appliesTo;
        StateTaxRate = stateTaxRate;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static TaxRate Create(
        string? code, 
        string? name, 
        decimal rateValue, 
        string? rateType, 
        decimal? minAmount, 
        decimal? maxAmount, 
        DateOnly effectiveDate, 
        DateOnly? endDate, 
        string? category, 
        string? description, 
        string? appliesTo, 
        int stateTaxRate, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new TaxRate(
            code,
            name,
            rateValue,
            rateType,
            minAmount,
            maxAmount,
            effectiveDate,
            endDate,
            category,
            description,
            appliesTo,
            stateTaxRate,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        decimal rateValue, 
        string? rateType, 
        decimal? minAmount, 
        decimal? maxAmount, 
        DateOnly effectiveDate, 
        DateOnly? endDate, 
        string? category, 
        string? description, 
        string? appliesTo, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        RateValue = rateValue;
        RateType = rateType;
        MinAmount = minAmount;
        MaxAmount = maxAmount;
        EffectiveDate = effectiveDate;
        EndDate = endDate;
        Category = category;
        Description = description;
        AppliesTo = appliesTo;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateTaxRate = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
