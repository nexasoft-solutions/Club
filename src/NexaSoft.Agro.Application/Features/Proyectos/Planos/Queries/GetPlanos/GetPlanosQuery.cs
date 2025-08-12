using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlanos;

public sealed record GetPlanosQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PlanoResponse>>;
