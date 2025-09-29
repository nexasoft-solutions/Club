using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Reservations.Events;

public sealed record ReservationUpdateDomainEvent
(
    long Id
): IDomainEvent;
