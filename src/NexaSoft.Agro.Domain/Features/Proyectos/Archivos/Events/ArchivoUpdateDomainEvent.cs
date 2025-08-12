using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Archivos.Events;

public sealed record ArchivoUpdateDomainEvent
(
    Guid Id
): IDomainEvent;
