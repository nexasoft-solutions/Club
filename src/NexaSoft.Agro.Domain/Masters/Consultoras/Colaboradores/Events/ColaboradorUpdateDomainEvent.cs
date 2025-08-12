using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores.Events;

public sealed record ColaboradorUpdateDomainEvent
(
    Guid Id
): IDomainEvent;
