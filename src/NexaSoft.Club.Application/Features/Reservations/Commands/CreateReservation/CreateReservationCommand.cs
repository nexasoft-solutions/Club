using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

public sealed record CreateReservationCommand(
    long MemberId,
    long SpaceRateId,
    long SpaceAvailabilityId,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    long? StatusId,
    long PaymentMethodId,
    string? ReferenceNumber,
    long DocumentTypeId,
    string? ReceiptNumber,
    decimal TotalAmount,
    string CreatedBy
) : ICommand<ReservationResponse>;


public sealed record ReservationResponse(
    long ReservationId,
    string ReservationNumber,
    decimal TotalAmount,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Status,
    string PaymentStatus,
    string AccountingStatus
);
