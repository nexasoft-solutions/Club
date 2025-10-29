using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Queries.GetTimeRequests;

public class GetTimeRequestsQueryHandler(
    IGenericRepository<TimeRequest> _repository
) : IQueryHandler<GetTimeRequestsQuery, Pagination<TimeRequestResponse>>
{
    public async Task<Result<Pagination<TimeRequestResponse>>> Handle(GetTimeRequestsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new TimeRequestSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<TimeRequestResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<TimeRequestResponse>(
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
            return Result.Failure<Pagination<TimeRequestResponse>>(TimeRequestErrores.ErrorConsulta);
        }
    }
}
