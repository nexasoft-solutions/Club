using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosAmbientales;

public class GetEstudiosAmbientalesQueryHandler(
    IEstudioAmbientalRepository _repository
) : IQueryHandler<GetEstudiosAmbientalesQuery, Pagination<EstudioAmbientalResponse>>
{
    public async Task<Result<Pagination<EstudioAmbientalResponse>>> Handle(GetEstudiosAmbientalesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EstudioAmbientalSpecification(query.SpecParams);
            var (pagination, _) = await _repository.GetEstudiosAmbientalesAsync(spec, cancellationToken);

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<EstudioAmbientalResponse>>(EstudioAmbientalErrores.ErrorConsulta);
        }
    }
}
