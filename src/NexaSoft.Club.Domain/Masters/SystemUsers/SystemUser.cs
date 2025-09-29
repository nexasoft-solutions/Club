using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.SystemUsers;

public class SystemUser : Entity
{
    public string? Username { get; private set; }
    public string? Email { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Role { get; private set; }
    public bool IsActive { get; private set; }
    public int StateSystemUser { get; private set; }

    private SystemUser() { }

    private SystemUser(
        string? username, 
        string? email, 
        string? firstName, 
        string? lastName, 
        string? role, 
        bool isActive, 
        int stateSystemUser, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        IsActive = isActive;
        StateSystemUser = stateSystemUser;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static SystemUser Create(
        string? username, 
        string? email, 
        string? firstName, 
        string? lastName, 
        string? role, 
        bool isActive, 
        int stateSystemUser, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new SystemUser(
            username,
            email,
            firstName,
            lastName,
            role,
            isActive,
            stateSystemUser,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? username, 
        string? email, 
        string? firstName, 
        string? lastName, 
        string? role, 
        bool isActive, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        IsActive = isActive;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSystemUser = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
