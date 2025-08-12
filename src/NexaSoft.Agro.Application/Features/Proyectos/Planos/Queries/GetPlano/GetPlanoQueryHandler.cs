using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlano;

public class GetPlanoQueryHandler(
    IPlanoRepository _repository
) : IQueryHandler<GetPlanoQuery, PlanoResponse>
{
    public async Task<Result<PlanoResponse>> Handle(GetPlanoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PlanoSpecification(specParams);

            var (pagination, _) = await _repository.GetPlanosAsync(spec, cancellationToken);

            var entity = pagination.Data.FirstOrDefault();

            if (entity is null)
               return Result.Failure<PlanoResponse>(PlanoErrores.NoEncontrado);

           return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PlanoResponse>(PlanoErrores.ErrorConsulta);
        }
    }
}
