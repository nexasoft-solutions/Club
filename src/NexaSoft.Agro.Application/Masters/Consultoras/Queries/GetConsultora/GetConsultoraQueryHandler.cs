using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Consultoras;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Queries.GetConsultora;

public class GetConsultoraQueryHandler(
    IGenericRepository<Consultora> _repository
) : IQueryHandler<GetConsultoraQuery, ConsultoraResponse>
{
    public async Task<Result<ConsultoraResponse>> Handle(GetConsultoraQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ConsultoraSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<ConsultoraResponse>(ConsultoraErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ConsultoraResponse>(ConsultoraErrores.ErrorConsulta);
        }
    }
}
