using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SpaceRates;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRates;

public class GetSpaceRatesQueryHandler(
    IGenericRepository<SpaceRate> _repository
) : IQueryHandler<GetSpaceRatesQuery, Pagination<SpaceRateResponse>>
{
    public async Task<Result<Pagination<SpaceRateResponse>>> Handle(GetSpaceRatesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SpaceRateSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SpaceRateResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SpaceRateResponse>(
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
            return Result.Failure<Pagination<SpaceRateResponse>>(SpaceRateErrores.ErrorConsulta);
        }
    }
}
