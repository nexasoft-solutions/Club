using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.LegalParameters;

public class LegalParameter : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public decimal Value { get; private set; }
    public string? ValueText { get; private set; }
    public DateOnly EffectiveDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public string? Category { get; private set; }
    public string? Description { get; private set; }
    public int StateLegalParameter { get; private set; }

    private LegalParameter() { }

    private LegalParameter(
        string? code, 
        string? name, 
        decimal value, 
        string? valueText, 
        DateOnly effectiveDate, 
        DateOnly? endDate, 
        string? category, 
        string? description, 
        int stateLegalParameter, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Value = value;
        ValueText = valueText;
        EffectiveDate = effectiveDate;
        EndDate = endDate;
        Category = category;
        Description = description;
        StateLegalParameter = stateLegalParameter;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static LegalParameter Create(
        string? code, 
        string? name, 
        decimal value, 
        string? valueText, 
        DateOnly effectiveDate, 
        DateOnly? endDate, 
        string? category, 
        string? description, 
        int stateLegalParameter, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new LegalParameter(
            code,
            name,
            value,
            valueText,
            effectiveDate,
            endDate,
            category,
            description,
            stateLegalParameter,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        decimal value, 
        string? valueText, 
        DateOnly effectiveDate, 
        DateOnly? endDate, 
        string? category, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        Value = value;
        ValueText = valueText;
        EffectiveDate = effectiveDate;
        EndDate = endDate;
        Category = category;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateLegalParameter = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
