using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Consultoras;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Queries.GetConsultoras;

public class GetConsultorasQueryHandler(
    IGenericRepository<Consultora> _repository
) : IQueryHandler<GetConsultorasQuery, Pagination<ConsultoraResponse>>
{
    public async Task<Result<Pagination<ConsultoraResponse>>> Handle(GetConsultorasQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ConsultoraSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ConsultoraResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ConsultoraResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<ConsultoraResponse>>(ConsultoraErrores.ErrorConsulta);
        }
    }
}
