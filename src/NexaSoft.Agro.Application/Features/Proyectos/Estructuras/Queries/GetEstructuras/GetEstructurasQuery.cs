using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Queries.GetEstructuras;

public sealed record GetEstructurasQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EstructuraResponse>>;
