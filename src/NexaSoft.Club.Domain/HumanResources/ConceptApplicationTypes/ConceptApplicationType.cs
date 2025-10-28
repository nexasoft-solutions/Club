using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;

public class ConceptApplicationType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StateConceptApplicationType { get; private set; }

    private ConceptApplicationType() { }

    private ConceptApplicationType(
        string? code, 
        string? name, 
        string? description, 
        int stateConceptApplicationType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Description = description;
        StateConceptApplicationType = stateConceptApplicationType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static ConceptApplicationType Create(
        string? code, 
        string? name, 
        string? description, 
        int stateConceptApplicationType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new ConceptApplicationType(
            code,
            name,
            description,
            stateConceptApplicationType,
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
        StateConceptApplicationType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
