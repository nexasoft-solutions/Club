using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Ubigeos;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Queries.GetUbigeos;

public class GetUbigeosQueryHandler(
    IGenericRepository<Ubigeo> _repository
) : IQueryHandler<GetUbigeosQuery, Pagination<UbigeoResponse>>
{
    public async Task<Result<Pagination<UbigeoResponse>>> Handle(GetUbigeosQuery query, CancellationToken cancellationToken)
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
