using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Events;

public sealed record EstudioAmbientalUpdateDomainEvent
(
    long Id
): IDomainEvent;
