using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Reservations.Events;

public sealed record ReservationCreateDomainEvent
(
    long Id
): IDomainEvent;
