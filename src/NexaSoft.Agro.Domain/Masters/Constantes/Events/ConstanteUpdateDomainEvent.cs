using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Constantes.Events;

public sealed record ConstanteUpdateDomainEvent
(
    long Id
): IDomainEvent;
