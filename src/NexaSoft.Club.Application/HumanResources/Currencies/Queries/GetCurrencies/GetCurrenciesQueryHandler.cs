using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Currencies;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Queries.GetCurrencies;

public class GetCurrenciesQueryHandler(
    IGenericRepository<Currency> _repository
) : IQueryHandler<GetCurrenciesQuery, Pagination<CurrencyResponse>>
{
    public async Task<Result<Pagination<CurrencyResponse>>> Handle(GetCurrenciesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CurrencySpecification(query.SpecParams);
            var responses = await _repository.ListAsync<CurrencyResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<CurrencyResponse>(
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
            return Result.Failure<Pagination<CurrencyResponse>>(CurrencyErrores.ErrorConsulta);
        }
    }
}
