using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Capitulos.Events;

public sealed record CapituloUpdateDomainEvent
(
    long Id
): IDomainEvent;
