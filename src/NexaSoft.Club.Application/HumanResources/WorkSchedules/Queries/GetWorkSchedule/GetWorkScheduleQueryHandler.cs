using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Queries.GetWorkSchedule;

public class GetWorkScheduleQueryHandler(
    IGenericRepository<WorkSchedule> _repository
) : IQueryHandler<GetWorkScheduleQuery, WorkScheduleResponse>
{
    public async Task<Result<WorkScheduleResponse>> Handle(GetWorkScheduleQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new WorkScheduleSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<WorkScheduleResponse>(WorkScheduleErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<WorkScheduleResponse>(WorkScheduleErrores.ErrorConsulta);
        }
    }
}
