using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores.Events;

public sealed record ColaboradorCreateDomainEvent
(
    long Id
): IDomainEvent;
