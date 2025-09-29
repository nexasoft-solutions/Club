using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.MemberTypes;
using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Domain.Features.Members;

public class Member : Entity
{
    public string? Dni { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public long MemberTypeId { get; private set; }
    public MemberType? MemberType { get; private set; }
    public long MemberStatusId { get; private set; }
    public MemberStatus? MemberStatus { get; private set; }
    public DateOnly JoinDate { get; private set; }
    public DateOnly? ExpirationDate { get; private set; }
    public decimal Balance { get; private set; }
    public string? QrCode { get; private set; }
    public DateTime? QrExpiration { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public int StateMember { get; private set; }
    public bool EntryFeePaid  { get; private set; }
    public DateTime? LastPaymentDate  { get; private set; }

    private Member() { }

    private Member(
        string? dni,
        string? firstName,
        string? lastName,
        string? email,
        string? phone,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long memberStatusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        string? qrCode,
        DateTime? qrExpiration,
        string? profilePictureUrl,
        int stateMember,
        bool entryFeePaid,
        DateTime? lastPaymentDate,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        BirthDate = birthDate;
        MemberTypeId = memberTypeId;
        MemberStatusId = memberStatusId;
        JoinDate = joinDate;
        ExpirationDate = expirationDate;
        Balance = balance;
        QrCode = qrCode;
        QrExpiration = qrExpiration;
        ProfilePictureUrl = profilePictureUrl;
        StateMember = stateMember;
        EntryFeePaid = entryFeePaid;
        LastPaymentDate = lastPaymentDate;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Member Create(
        string? dni,
        string? firstName,
        string? lastName,
        string? email,
        string? phone,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long statusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        string? qrCode,
        DateTime? qrExpiration,
        string? profilePictureUrl,
        int stateMember,
        bool entryFeePaid,
        DateTime? lastPaymentDate,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Member(
            dni,
            firstName,
            lastName,
            email,
            phone,
            address,
            birthDate,
            memberTypeId,
            statusId,
            joinDate,
            expirationDate,
            balance,
            qrCode,
            qrExpiration,
            profilePictureUrl,
            stateMember,
            entryFeePaid,
            lastPaymentDate,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? dni,
        string? firstName,
        string? lastName,
        string? email,
        string? phone,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long statusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        string? qrCode,
        DateTime? qrExpiration,
        string? profilePictureUrl,
        bool entryFeePaid,
        DateTime? lastPaymentDate,
        DateTime utcNow,
        string? updatedBy
    )
    {
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        BirthDate = birthDate;
        MemberTypeId = memberTypeId;
        MemberStatusId = statusId;
        JoinDate = joinDate;
        ExpirationDate = expirationDate;
        Balance = balance;
        QrCode = qrCode;
        QrExpiration = qrExpiration;
        ProfilePictureUrl = profilePictureUrl;
        EntryFeePaid = entryFeePaid;
        LastPaymentDate = lastPaymentDate;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateMember = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    public void UpdateBalance(decimal paymentAmount, string updatedBy)
    {
        Balance += paymentAmount; // Aumentar balance (si era negativo, se reduce la deuda)
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }    
}
