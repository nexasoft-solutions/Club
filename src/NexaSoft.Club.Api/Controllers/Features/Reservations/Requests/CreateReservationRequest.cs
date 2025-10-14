using Org.BouncyCastle.Asn1.Cms;

namespace NexaSoft.Club.Api.Controllers.Features.Reservations.Request;

public sealed record CreateReservationRequest(
    long MemberId,
    long SpaceRateId,
    long SpaceAvailabilityId,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    long PaymentMethodId,
    string? ReferenceNumber,
    long DocumentTypeId,
    string? ReceiptNumber,
    decimal TotalAmount,
    string CreatedBy
);
