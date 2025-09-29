using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SpaceRates;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRate;

public class GetSpaceRateQueryHandler(
    IGenericRepository<SpaceRate> _repository
) : IQueryHandler<GetSpaceRateQuery, SpaceRateResponse>
{
    public async Task<Result<SpaceRateResponse>> Handle(GetSpaceRateQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new SpaceRateSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<SpaceRateResponse>(SpaceRateErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SpaceRateResponse>(SpaceRateErrores.ErrorConsulta);
        }
    }
}
