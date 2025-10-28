using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetTreeAccountingChart;

public class GetTreeAccountingChartQueryHandler(
    IGenericRepository<AccountingChart> _repository
)  : IQueryHandler<GetTreeAccountingChartQuery, List<AccountingChartResponse>>
{
    public async Task<Result<List<AccountingChartResponse>>> Handle(GetTreeAccountingChartQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { NoPaging = true };
            var spec = new AccountingChartSpecification(specParams);
            var list = await _repository.ListAsync(spec, cancellationToken);
            if (list == null || !list.Any())
                return Result.Failure<List<AccountingChartResponse>>(AccountingChartErrores.NoEncontrado);

            // 🔹 Construir árbol jerárquico
            var tree = BuildAccountingChartTree(list);

            return Result.Success(tree);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<AccountingChartResponse>>(AccountingChartErrores.ErrorConsulta);
        }
    }

    private static List<AccountingChartResponse> BuildAccountingChartTree(IEnumerable<AccountingChartResponse> flatList)
    {
        var lookup = flatList.ToLookup(x => x.ParentAccountId);

        List<AccountingChartResponse> Build(long? parentId)
        {
            return lookup[parentId]
                .Select(x => x with
                {
                    Children = Build(x.Id)
                })
                .OrderBy(x => x.AccountCode)
                .ToList();
        }

        return Build(null); // Raíces (sin padre)
    }
}
