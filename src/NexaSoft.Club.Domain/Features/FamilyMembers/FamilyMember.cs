using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Features.FamilyMembers;

public class FamilyMember : Entity
{
    public long MemberId { get; private set; }
    public Member? Member { get; private set; }
    public string? Dni { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Relationship { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public bool IsAuthorized { get; private set; }
    public int StateFamilyMember { get; private set; }

    private FamilyMember() { }

    private FamilyMember(
        long memberId, 
        string? dni, 
        string? firstName, 
        string? lastName, 
        string? relationship, 
        DateOnly? birthDate, 
        bool isAuthorized, 
        int stateFamilyMember, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        MemberId = memberId;
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Relationship = relationship;
        BirthDate = birthDate;
        IsAuthorized = isAuthorized;
        StateFamilyMember = stateFamilyMember;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static FamilyMember Create(
        long memberId, 
        string? dni, 
        string? firstName, 
        string? lastName, 
        string? relationship, 
        DateOnly? birthDate, 
        bool isAuthorized, 
        int stateFamilyMember, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new FamilyMember(
            memberId,
            dni,
            firstName,
            lastName,
            relationship,
            birthDate,
            isAuthorized,
            stateFamilyMember,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long memberId, 
        string? dni, 
        string? firstName, 
        string? lastName, 
        string? relationship, 
        DateOnly? birthDate, 
        bool isAuthorized, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        MemberId = memberId;
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Relationship = relationship;
        BirthDate = birthDate;
        IsAuthorized = isAuthorized;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateFamilyMember = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
