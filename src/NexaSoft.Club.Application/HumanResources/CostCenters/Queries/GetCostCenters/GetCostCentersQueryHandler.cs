using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.CostCenters;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Queries.GetCostCenters;

public class GetCostCentersQueryHandler(
    IGenericRepository<CostCenter> _repository
) : IQueryHandler<GetCostCentersQuery, Pagination<CostCenterResponse>>
{
    public async Task<Result<Pagination<CostCenterResponse>>> Handle(GetCostCentersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CostCenterSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<CostCenterResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<CostCenterResponse>(
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
            return Result.Failure<Pagination<CostCenterResponse>>(CostCenterErrores.ErrorConsulta);
        }
    }
}
