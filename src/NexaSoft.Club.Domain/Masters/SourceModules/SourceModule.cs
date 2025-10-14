using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.SourceModules;

public class SourceModule : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StateSourceModule { get; private set; }

    private SourceModule() { }

    private SourceModule(
        string? name, 
        string? description, 
        int stateSourceModule, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        StateSourceModule = stateSourceModule;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static SourceModule Create(
        string? name, 
        string? description, 
        int stateSourceModule, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new SourceModule(
            name,
            description,
            stateSourceModule,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? name, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Name = name;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSourceModule = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
