using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.TaxRates;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Queries.GetTaxRates;

public class GetTaxRatesQueryHandler(
    IGenericRepository<TaxRate> _repository
) : IQueryHandler<GetTaxRatesQuery, Pagination<TaxRateResponse>>
{
    public async Task<Result<Pagination<TaxRateResponse>>> Handle(GetTaxRatesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new TaxRateSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<TaxRateResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<TaxRateResponse>(
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
            return Result.Failure<Pagination<TaxRateResponse>>(TaxRateErrores.ErrorConsulta);
        }
    }
}
