using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.SpecialRates;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Queries.GetSpecialRates;

public class GetSpecialRatesQueryHandler(
    IGenericRepository<SpecialRate> _repository
) : IQueryHandler<GetSpecialRatesQuery, Pagination<SpecialRateResponse>>
{
    public async Task<Result<Pagination<SpecialRateResponse>>> Handle(GetSpecialRatesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SpecialRateSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SpecialRateResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SpecialRateResponse>(
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
            return Result.Failure<Pagination<SpecialRateResponse>>(SpecialRateErrores.ErrorConsulta);
        }
    }
}
