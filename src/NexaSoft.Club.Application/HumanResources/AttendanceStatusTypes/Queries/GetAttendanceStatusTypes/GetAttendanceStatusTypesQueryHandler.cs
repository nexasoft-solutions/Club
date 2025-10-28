using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Queries.GetAttendanceStatusTypes;

public class GetAttendanceStatusTypesQueryHandler(
    IGenericRepository<AttendanceStatusType> _repository
) : IQueryHandler<GetAttendanceStatusTypesQuery, Pagination<AttendanceStatusTypeResponse>>
{
    public async Task<Result<Pagination<AttendanceStatusTypeResponse>>> Handle(GetAttendanceStatusTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new AttendanceStatusTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<AttendanceStatusTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<AttendanceStatusTypeResponse>(
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
            return Result.Failure<Pagination<AttendanceStatusTypeResponse>>(AttendanceStatusTypeErrores.ErrorConsulta);
        }
    }
}
