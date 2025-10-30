using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

public class IncomeTaxScale : Entity
{
    public int ScaleYear { get; private set; }
    public decimal MinIncome { get; private set; }
    public decimal? MaxIncome { get; private set; }
    public decimal FixedAmount { get; private set; }
    public decimal Rate { get; private set; }
    public decimal ExcessOver { get; private set; }
    public string? Description { get; private set; }
    public int StateIncomeTaxScale { get; private set; }

    private IncomeTaxScale() { }

    private IncomeTaxScale(
        int scaleYear, 
        decimal minIncome, 
        decimal? maxIncome, 
        decimal fixedAmount, 
        decimal rate, 
        decimal excessOver, 
        string? description, 
        int stateIncomeTaxScale, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        ScaleYear = scaleYear;
        MinIncome = minIncome;
        MaxIncome = maxIncome;
        FixedAmount = fixedAmount;
        Rate = rate;
        ExcessOver = excessOver;
        Description = description;
        StateIncomeTaxScale = stateIncomeTaxScale;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static IncomeTaxScale Create(
        int scaleYear, 
        decimal minIncome, 
        decimal? maxIncome, 
        decimal fixedAmount, 
        decimal rate, 
        decimal excessOver, 
        string? description, 
        int stateIncomeTaxScale, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new IncomeTaxScale(
            scaleYear,
            minIncome,
            maxIncome,
            fixedAmount,
            rate,
            excessOver,
            description,
            stateIncomeTaxScale,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        int scaleYear, 
        decimal minIncome, 
        decimal? maxIncome, 
        decimal fixedAmount, 
        decimal rate, 
        decimal excessOver, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        ScaleYear = scaleYear;
        MinIncome = minIncome;
        MaxIncome = maxIncome;
        FixedAmount = fixedAmount;
        Rate = rate;
        ExcessOver = excessOver;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateIncomeTaxScale = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
