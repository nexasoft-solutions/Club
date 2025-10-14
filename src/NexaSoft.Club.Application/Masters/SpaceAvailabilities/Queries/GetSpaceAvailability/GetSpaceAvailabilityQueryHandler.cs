using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Queries.GetSpaceAvailability;

public class GetSpaceAvailabilityQueryHandler(
    IGenericRepository<SpaceAvailability> _repository
) : IQueryHandler<GetSpaceAvailabilityQuery, SpaceAvailabilityResponse>
{
    public async Task<Result<SpaceAvailabilityResponse>> Handle(GetSpaceAvailabilityQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new SpaceAvailabilitySpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<SpaceAvailabilityResponse>(SpaceAvailabilityErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SpaceAvailabilityResponse>(SpaceAvailabilityErrores.ErrorConsulta);
        }
    }
}
