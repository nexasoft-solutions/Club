using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

public sealed record CreateReservationCommand(
    long MemberId,
    long SpaceId,
    DateTime StartTime,
    DateTime EndTime,
    string? Status,
    decimal TotalAmount,
    long? AccountingEntryId,
    string CreatedBy
) : ICommand<long>;
