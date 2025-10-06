namespace NexaSoft.Club.Domain.Features.Members;

public sealed record MemberResponse(
    long Id,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Address,
    DateOnly? BirthDate,
    long MemberTypeId,
    string? TypeName,
    long MemberStatusId,
    string? StatusName,
    DateOnly JoinDate,
    DateOnly? ExpirationDate,
    decimal Balance,
    string? QrCode,
    DateOnly? QrExpiration,
    string? ProfilePictureUrl,
    bool EntryFeePaid,
    DateTime? LastPaymentDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
