using NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

namespace NexaSoft.Club.Application.Features.Reservations.Services;

public interface IReservationBackgroundProcessor
{
    Task ProcessReservationAsync(long reservationId, CreateReservationCommand command, CancellationToken cancellationToken);
}
