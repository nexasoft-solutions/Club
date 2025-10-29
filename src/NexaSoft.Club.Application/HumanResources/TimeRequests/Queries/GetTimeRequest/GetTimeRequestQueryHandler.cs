using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Queries.GetTimeRequest;

public class GetTimeRequestQueryHandler(
    IGenericRepository<TimeRequest> _repository
) : IQueryHandler<GetTimeRequestQuery, TimeRequestResponse>
{
    public async Task<Result<TimeRequestResponse>> Handle(GetTimeRequestQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new TimeRequestSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<TimeRequestResponse>(TimeRequestErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<TimeRequestResponse>(TimeRequestErrores.ErrorConsulta);
        }
    }
}
