using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlanos;

public class GetPlanosQueryHandler(
    IPlanoRepository _repository
) : IQueryHandler<GetPlanosQuery, Pagination<PlanoResponse>>
{
    public async Task<Result<Pagination<PlanoResponse>>> Handle(GetPlanosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PlanoSpecification(query.SpecParams);
            var (pagination, _) = await _repository.GetPlanosAsync(spec, cancellationToken);

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<PlanoResponse>>(PlanoErrores.ErrorConsulta);
        }
    }
}
