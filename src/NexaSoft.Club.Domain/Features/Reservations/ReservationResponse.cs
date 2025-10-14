namespace NexaSoft.Club.Domain.Features.Reservations;

public sealed record ReservationResponse(
    long Id,
    long MemberId,
    string? MemberFullName,
    string? MemberTypeName,
    long SpaceRateId,
    string? SpaceRateName,
    long SpaceAvailabilityId,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    long? StatusId,
    string? StatusDescription,
    long PaymentMethodId,
    string? PaymentMethodDescription,
    string? ReferenceNumber,
    long DocumentTypeId,
    string? DocumentTypeDescription,
    string? ReceiptNumber,
    decimal TotalAmount,
    long? AccountingEntryId,
    string? EntryNumber,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
