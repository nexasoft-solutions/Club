
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbientalById;

public class GetEstudioAmbientalByIdQueryHandler(IEstudioAmbientalRepository _repository) : IQueryHandler<GetEstudioAmbientalByIdQuery, EstudioAmbientalDtoResponse?>
{
    public async Task<Result<EstudioAmbientalDtoResponse?>> Handle(GetEstudioAmbientalByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.GetEstudioAmbientalByIdAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EstudioAmbientalDtoResponse?>(EstudioAmbientalErrores.ErrorConsulta);
        }

    }
}
