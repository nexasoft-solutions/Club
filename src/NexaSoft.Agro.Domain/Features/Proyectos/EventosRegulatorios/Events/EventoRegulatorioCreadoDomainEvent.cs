using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;

public record class EventoRegulatorioCreadoDomainEvent
(
    long Id
): IDomainEvent;
