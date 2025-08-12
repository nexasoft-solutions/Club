using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Ubigeos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Queries.GetUbigeos;

public class GetUbigeosQueryHandler(
    IGenericRepository<Ubigeo> _repository
) : IQueryHandler<GetUbigeosQuery, Result<Pagination<UbigeoResponse>>>
{
    public async Task<Result<Result<Pagination<UbigeoResponse>>>> Handle(GetUbigeosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new UbigeoSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<UbigeoResponse>(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<UbigeoResponse>(
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
            return Result.Failure<Pagination<UbigeoResponse>>(UbigeoErrores.ErrorConsulta);
        }
    }
}
