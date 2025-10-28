using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Queries.GetCostCenterTypes;

public class GetCostCenterTypesQueryHandler(
    IGenericRepository<CostCenterType> _repository
) : IQueryHandler<GetCostCenterTypesQuery, Pagination<CostCenterTypeResponse>>
{
    public async Task<Result<Pagination<CostCenterTypeResponse>>> Handle(GetCostCenterTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CostCenterTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<CostCenterTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<CostCenterTypeResponse>(
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
            return Result.Failure<Pagination<CostCenterTypeResponse>>(CostCenterTypeErrores.ErrorConsulta);
        }
    }
}
