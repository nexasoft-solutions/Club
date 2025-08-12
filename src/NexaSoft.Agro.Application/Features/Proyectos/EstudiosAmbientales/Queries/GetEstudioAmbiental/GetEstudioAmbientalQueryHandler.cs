using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbiental;

public class GetEstudioAmbientalQueryHandler(
    IEstudioAmbientalRepository _repository
) : IQueryHandler<GetEstudioAmbientalQuery, EstudioAmbientalResponse>
{
    public async Task<Result<EstudioAmbientalResponse>> Handle(GetEstudioAmbientalQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EstudioAmbientalSpecification(specParams);

            var (pagination, _) = await _repository.GetEstudiosAmbientalesAsync(spec, cancellationToken);

            var entity = pagination.Data.FirstOrDefault();

            if (entity is null)
               return Result.Failure<EstudioAmbientalResponse>(EstudioAmbientalErrores.NoEncontrado);

           return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EstudioAmbientalResponse>(EstudioAmbientalErrores.ErrorConsulta);
        }
    }
}
