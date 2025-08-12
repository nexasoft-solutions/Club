using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras;

public interface IEstructuraRepository
{
   Task<(Pagination<EstructuraResponse> Items, int TotalItems)> GetEstructurasAsync (ISpecification<Estructura> spec, CancellationToken cancellationToken); 
}
