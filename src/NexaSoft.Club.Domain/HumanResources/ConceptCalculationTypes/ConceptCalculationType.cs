using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;

public class ConceptCalculationType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StateConceptCalculationType { get; private set; }

    private ConceptCalculationType() { }

    private ConceptCalculationType(
        string? code, 
        string? name, 
        string? description, 
        int stateConceptCalculationType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Description = description;
        StateConceptCalculationType = stateConceptCalculationType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static ConceptCalculationType Create(
        string? code, 
        string? name, 
        string? description, 
        int stateConceptCalculationType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new ConceptCalculationType(
            code,
            name,
            description,
            stateConceptCalculationType,
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
        StateConceptCalculationType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
