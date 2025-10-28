using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Currencies;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Queries.GetCurrency;

public class GetCurrencyQueryHandler(
    IGenericRepository<Currency> _repository
) : IQueryHandler<GetCurrencyQuery, CurrencyResponse>
{
    public async Task<Result<CurrencyResponse>> Handle(GetCurrencyQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new CurrencySpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<CurrencyResponse>(CurrencyErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<CurrencyResponse>(CurrencyErrores.ErrorConsulta);
        }
    }
}
