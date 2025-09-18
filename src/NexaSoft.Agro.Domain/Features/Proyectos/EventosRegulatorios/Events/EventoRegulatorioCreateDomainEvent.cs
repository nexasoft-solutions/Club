using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;

public sealed record EventoRegulatorioCreateDomainEvent
(
    long Id
): IDomainEvent;
