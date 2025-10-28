using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Queries.GetTimeRequestTypes;

public class GetTimeRequestTypesQueryHandler(
    IGenericRepository<TimeRequestType> _repository
) : IQueryHandler<GetTimeRequestTypesQuery, Pagination<TimeRequestTypeResponse>>
{
    public async Task<Result<Pagination<TimeRequestTypeResponse>>> Handle(GetTimeRequestTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new TimeRequestTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<TimeRequestTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<TimeRequestTypeResponse>(
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
            return Result.Failure<Pagination<TimeRequestTypeResponse>>(TimeRequestTypeErrores.ErrorConsulta);
        }
    }
}
