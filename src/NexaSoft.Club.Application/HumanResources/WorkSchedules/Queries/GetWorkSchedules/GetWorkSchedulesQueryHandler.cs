using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Queries.GetWorkSchedules;

public class GetWorkSchedulesQueryHandler(
    IGenericRepository<WorkSchedule> _repository
) : IQueryHandler<GetWorkSchedulesQuery, Pagination<WorkScheduleResponse>>
{
    public async Task<Result<Pagination<WorkScheduleResponse>>> Handle(GetWorkSchedulesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new WorkScheduleSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<WorkScheduleResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<WorkScheduleResponse>(
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
            return Result.Failure<Pagination<WorkScheduleResponse>>(WorkScheduleErrores.ErrorConsulta);
        }
    }
}
