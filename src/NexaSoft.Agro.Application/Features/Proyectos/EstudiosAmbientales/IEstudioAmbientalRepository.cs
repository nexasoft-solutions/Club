using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales;

public interface IEstudioAmbientalRepository
{
   Task<(Pagination<EstudioAmbientalResponse> Items, int TotalItems)> GetEstudiosAmbientalesAsync(ISpecification<EstudioAmbiental> spec, CancellationToken cancellationToken);

   Task<EstudioAmbientalDtoResponse?> GetEstudioAmbientalByIdAsync(long id, CancellationToken cancellationToken);

   Task<EstudioAmbientalCapituloResponse> GetEstudioAmbientalCapitulosByIdAsync(long id, CancellationToken cancellationToken);

   Task<List<EstudioAmbientalEstructuraResponse>> GetEstudioAmbientalEstructurasByIdAsync(long id, CancellationToken cancellationToken);

}
