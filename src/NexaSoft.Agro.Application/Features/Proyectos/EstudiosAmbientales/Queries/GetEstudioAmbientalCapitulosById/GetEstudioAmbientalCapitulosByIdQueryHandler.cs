using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbientalCapitulosById;

public class GetEstudioAmbientalCapitulosByIdQueryHandler(IEstudioAmbientalRepository _repository) : IQueryHandler<GetEstudioAmbientalCapitulosByIdQuery, EstudioAmbientalCapituloResponse>
{
    public async Task<Result<EstudioAmbientalCapituloResponse>> Handle(GetEstudioAmbientalCapitulosByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.GetEstudioAmbientalCapitulosByIdAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EstudioAmbientalCapituloResponse>(EstudioAmbientalErrores.ErrorConsulta);
        }
    }
}
