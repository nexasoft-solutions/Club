using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Queries.GetResponsables;

public sealed record GetResponsablesQuery(
    BaseSpecParams<long> SpecParams
) : IQuery<Pagination<ResponsableResponse>>;
