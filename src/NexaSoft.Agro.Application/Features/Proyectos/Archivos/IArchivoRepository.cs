using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos;

public interface IArchivoRepository
{
   Task<(Pagination<ArchivoResponse> Items, int TotalItems)> GetArchivosAsync(ISpecification<Archivo> spec, CancellationToken cancellationToken);

   Task<List<ArchivoItemResponse>> GetArchivosByNombreCortoAsync(long estudioAmbientalId,string filtro, CancellationToken cancellationToken);
}
