using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.UserTypes;

public class UserType : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsAdministrative { get; private set; }
    public int StateUserType { get; private set; }

    private UserType() { }

    private UserType(
        string? name, 
        string? description, 
        bool isAdministrative, 
        int stateUserType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        IsAdministrative = isAdministrative;
        StateUserType = stateUserType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static UserType Create(
        string? name, 
        string? description, 
        bool isAdministrative, 
        int stateUserType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new UserType(
            name,
            description,
            isAdministrative,
            stateUserType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? name, 
        string? description, 
        bool isAdministrative, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Name = name;
        Description = description;
        IsAdministrative = isAdministrative;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateUserType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
