using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Statuses;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Statuses.Queries.GetStatus;

public class GetStatusQueryHandler(
    IGenericRepository<Status> _repository
) : IQueryHandler<GetStatusQuery, StatusResponse>
{
    public async Task<Result<StatusResponse>> Handle(GetStatusQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new StatusSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<StatusResponse>(StatusErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<StatusResponse>(StatusErrores.ErrorConsulta);
        }
    }
}
