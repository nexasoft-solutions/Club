using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Queries.GetEventosRegulatorios;

public sealed record GetEventosRegulatoriosQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EventoRegulatorioResponse>>;
