using NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

namespace NexaSoft.Club.Application.Features.Reservations.Background;

public interface IReservationBackgroundTaskService
{
    Task QueueReservationProcessingAsync(long reservationId, CreateReservationCommand command, CancellationToken cancellationToken = default);
}
