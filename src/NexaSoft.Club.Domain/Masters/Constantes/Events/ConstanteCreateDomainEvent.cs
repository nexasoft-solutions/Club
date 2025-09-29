using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Constantes.Events;

public sealed record ConstanteCreateDomainEvent
(
    long Id
): IDomainEvent;
