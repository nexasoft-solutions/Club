using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Queries.GetEventoRegulatorio;

public sealed record GetEventoRegulatorioQuery(
    long Id
) : IQuery<EventoRegulatorioResponse>;
