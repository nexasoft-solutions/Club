using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpaceRates;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRateByMemberType;

public class GetSpaceRateByMemberTypeQueryHandler(
    IGenericRepository<SpaceRate> _repository
) : IQueryHandler<GetSpaceRateByMemberTypeQuery, SpaceRateResponse>
{
    public async Task<Result<SpaceRateResponse>> Handle(GetSpaceRateByMemberTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SpaceRateSpecification(query.SpaceId, query.MemberTypeId);
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
