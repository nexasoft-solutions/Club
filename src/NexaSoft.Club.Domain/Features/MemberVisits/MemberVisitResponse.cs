namespace NexaSoft.Club.Domain.Features.MemberVisits;

public sealed record MemberVisitResponse(
    long Id,
    long? MemberId,
    string? MemberFirstName,
    string? MemberLastName,
    DateOnly? VisitDate,
    TimeOnly? EntryTime,
    TimeOnly? ExitTime,
    string? QrCodeUsed,
    string? Notes,
    string? CheckInBy,
    string? CheckOutBy,
    int VisitType,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
