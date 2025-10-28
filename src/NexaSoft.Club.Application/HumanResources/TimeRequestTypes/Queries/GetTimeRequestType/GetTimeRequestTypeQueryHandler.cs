using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Queries.GetTimeRequestType;

public class GetTimeRequestTypeQueryHandler(
    IGenericRepository<TimeRequestType> _repository
) : IQueryHandler<GetTimeRequestTypeQuery, TimeRequestTypeResponse>
{
    public async Task<Result<TimeRequestTypeResponse>> Handle(GetTimeRequestTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new TimeRequestTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<TimeRequestTypeResponse>(TimeRequestTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<TimeRequestTypeResponse>(TimeRequestTypeErrores.ErrorConsulta);
        }
    }
}
