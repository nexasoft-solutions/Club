using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Application.Features.Organizaciones;

public interface IOrganizacionRepository
{
   Task<(Pagination<OrganizacionResponse> Items, int TotalItems)> GetOrganizacionesAsync(ISpecification<Organizacion> spec, CancellationToken cancellationToken);
}
