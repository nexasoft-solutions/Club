using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizaciones;

public sealed record GetOrganizacionesQuery(
    BaseSpecParams<int> SpecParams
) : IQuery<Pagination<OrganizacionResponse>>;
