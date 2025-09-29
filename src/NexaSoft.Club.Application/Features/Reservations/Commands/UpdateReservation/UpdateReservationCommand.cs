using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.UpdateReservation;

public sealed record UpdateReservationCommand(
    long Id,
    long MemberId,
    long SpaceId,
    DateTime StartTime,
    DateTime EndTime,
    string? Status,
    decimal TotalAmount,
    long? AccountingEntryId,
    string UpdatedBy
) : ICommand<bool>;
