using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Queries.GetCumplimientos;

public sealed record GetCumplimientosQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<CumplimientoResponse>>;
