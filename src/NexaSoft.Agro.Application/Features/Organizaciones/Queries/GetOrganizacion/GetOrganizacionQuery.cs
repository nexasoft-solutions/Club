using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizacion;

public sealed record GetOrganizacionQuery(
    Guid Id
) : IQuery<OrganizacionResponse>;
