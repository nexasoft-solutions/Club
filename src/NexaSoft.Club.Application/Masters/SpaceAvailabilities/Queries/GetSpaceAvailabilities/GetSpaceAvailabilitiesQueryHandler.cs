using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Queries.GetSpaceAvailabilities;

public class GetSpaceAvailabilitiesQueryHandler(
    IGenericRepository<SpaceAvailability> _repository
) : IQueryHandler<GetSpaceAvailabilitiesQuery, Pagination<SpaceAvailabilityResponse>>
{
    public async Task<Result<Pagination<SpaceAvailabilityResponse>>> Handle(GetSpaceAvailabilitiesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SpaceAvailabilitySpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SpaceAvailabilityResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SpaceAvailabilityResponse>(
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
            return Result.Failure<Pagination<SpaceAvailabilityResponse>>(SpaceAvailabilityErrores.ErrorConsulta);
        }
    }
}
