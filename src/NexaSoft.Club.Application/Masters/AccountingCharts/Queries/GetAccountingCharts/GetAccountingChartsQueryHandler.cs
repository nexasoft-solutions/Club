using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetAccountingCharts;

public class GetAccountingChartsQueryHandler(
    IGenericRepository<AccountingChart> _repository
) : IQueryHandler<GetAccountingChartsQuery, Pagination<AccountingChartResponse>>
{
    public async Task<Result<Pagination<AccountingChartResponse>>> Handle(GetAccountingChartsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new AccountingChartSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<AccountingChartResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<AccountingChartResponse>(
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
            return Result.Failure<Pagination<AccountingChartResponse>>(AccountingChartErrores.ErrorConsulta);
        }
    }
}
