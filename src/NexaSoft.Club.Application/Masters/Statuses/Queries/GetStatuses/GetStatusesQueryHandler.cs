using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Statuses;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Statuses.Queries.GetStatuses;

public class GetStatusesQueryHandler(
    IGenericRepository<Status> _repository
) : IQueryHandler<GetStatusesQuery, Pagination<StatusResponse>>
{
    public async Task<Result<Pagination<StatusResponse>>> Handle(GetStatusesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new StatusSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<StatusResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<StatusResponse>(
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
            return Result.Failure<Pagination<StatusResponse>>(StatusErrores.ErrorConsulta);
        }
    }
}
