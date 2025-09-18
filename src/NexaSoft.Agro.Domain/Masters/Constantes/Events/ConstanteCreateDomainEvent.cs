using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Constantes.Events;

public sealed record ConstanteCreateDomainEvent
(
    long Id
): IDomainEvent;
