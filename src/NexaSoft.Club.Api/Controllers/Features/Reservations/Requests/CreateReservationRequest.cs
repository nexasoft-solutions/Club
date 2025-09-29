namespace NexaSoft.Club.Api.Controllers.Features.Reservations.Request;

public sealed record CreateReservationRequest(
    long MemberId,
    long SpaceId,
    DateTime StartTime,
    DateTime EndTime,
    string? Status,
    decimal TotalAmount,
    long? AccountingEntryId,
    string CreatedBy
);
