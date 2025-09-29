using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.DeleteReservation;

public sealed record DeleteReservationCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
