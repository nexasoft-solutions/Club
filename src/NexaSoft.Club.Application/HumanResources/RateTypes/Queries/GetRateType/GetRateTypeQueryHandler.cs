using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.RateTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Queries.GetRateType;

public class GetRateTypeQueryHandler(
    IGenericRepository<RateType> _repository
) : IQueryHandler<GetRateTypeQuery, RateTypeResponse>
{
    public async Task<Result<RateTypeResponse>> Handle(GetRateTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new RateTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<RateTypeResponse>(RateTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<RateTypeResponse>(RateTypeErrores.ErrorConsulta);
        }
    }
}
