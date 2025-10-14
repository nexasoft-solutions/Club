using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.DocumentTypes;

public class DocumentType : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? Serie { get; private set; }
    public int StateDocumentType { get; private set; }

    private DocumentType() { }

    private DocumentType(
        string? name, 
        string? description, 
        string? serie,
        int stateDocumentType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        Serie = serie;
        StateDocumentType = stateDocumentType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static DocumentType Create(
        string? name, 
        string? description, 
        string? serie,
        int stateDocumentType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new DocumentType(
            name,
            description,
            serie,
            stateDocumentType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? name, 
        string? description,
        string? serie,
        DateTime utcNow,
        string? updatedBy
    )
    {
        Name = name;
        Description = description;
        Serie = serie;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateDocumentType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
