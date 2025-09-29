namespace NexaSoft.Club.Domain.Features.Reservations;

public sealed record ReservationResponse(
    long Id,
    long MemberId,
    string? MemberFirstName,
    string? MemberLastName,
    long SpaceId,
    string? SpaceName,
    DateTime StartTime,
    DateTime EndTime,
    string? Status,
    decimal TotalAmount,
    long? AccountingEntryId,
    string? EntryNumber,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
