using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioEstructurasById;

public class GetEstudioEstructurasByIdQueryHandler(IEstudioAmbientalRepository _repository)  : IQueryHandler<GetEstudioEstructurasByIdQuery, List<EstudioAmbientalEstructuraResponse>>
{
    public async Task<Result<List<EstudioAmbientalEstructuraResponse>>> Handle(GetEstudioEstructurasByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.GetEstudioAmbientalEstructurasByIdAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<EstudioAmbientalEstructuraResponse>>(EstudioAmbientalErrores.ErrorConsulta);
        }
    }
}
