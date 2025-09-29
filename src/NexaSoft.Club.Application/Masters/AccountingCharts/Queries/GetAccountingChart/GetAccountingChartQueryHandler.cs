using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetAccountingChart;

public class GetAccountingChartQueryHandler(
    IGenericRepository<AccountingChart> _repository
) : IQueryHandler<GetAccountingChartQuery, AccountingChartResponse>
{
    public async Task<Result<AccountingChartResponse>> Handle(GetAccountingChartQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new AccountingChartSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<AccountingChartResponse>(AccountingChartErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<AccountingChartResponse>(AccountingChartErrores.ErrorConsulta);
        }
    }
}
