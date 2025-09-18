using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos.Events;

public sealed record SubCapituloUpdateDomainEvent
(
    long Id
): IDomainEvent;
