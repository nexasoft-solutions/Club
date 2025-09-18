using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos;

public interface IPlanoRepository
{
   Task<Result<Plano>> GetPlanoByIdDetalle(long PlanoId, CancellationToken cancellationToken);

   Task<Result<PlanoItemResponse>> GetPlanoArchivoById(long ArchivoId, CancellationToken cancellationToken);

   Task<(Pagination<PlanoResponse> Items, int TotalItems)> GetPlanosAsync (ISpecification<Plano> spec, CancellationToken cancellationToken); 
}
