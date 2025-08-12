using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Events;

public sealed record EstudioAmbientalCreateDomainEvent
(
    Guid Id
): IDomainEvent;
