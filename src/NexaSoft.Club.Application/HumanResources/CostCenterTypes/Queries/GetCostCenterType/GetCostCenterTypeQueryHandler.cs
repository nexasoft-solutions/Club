using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Queries.GetCostCenterType;

public class GetCostCenterTypeQueryHandler(
    IGenericRepository<CostCenterType> _repository
) : IQueryHandler<GetCostCenterTypeQuery, CostCenterTypeResponse>
{
    public async Task<Result<CostCenterTypeResponse>> Handle(GetCostCenterTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new CostCenterTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<CostCenterTypeResponse>(CostCenterTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<CostCenterTypeResponse>(CostCenterTypeErrores.ErrorConsulta);
        }
    }
}
