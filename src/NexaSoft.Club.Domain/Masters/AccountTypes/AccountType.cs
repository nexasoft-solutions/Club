using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.AccountTypes;

public class AccountType : Entity
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StateAccountType { get; private set; }

    private AccountType() { }

    private AccountType(
        string? name, 
        string? description, 
        int stateAccountType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Name = name;
        Description = description;
        StateAccountType = stateAccountType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static AccountType Create(
        string? name, 
        string? description,
        int stateAccountType, 
        DateTime createdAt,
        string? createdBy
    )
    {
        var entity = new AccountType(
            name,
            description,
            stateAccountType,
            createdAt,
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
        StateAccountType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
