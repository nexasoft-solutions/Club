using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Queries.GetAttendanceRecords;

public class GetAttendanceRecordsQueryHandler(
    IGenericRepository<AttendanceRecord> _repository
) : IQueryHandler<GetAttendanceRecordsQuery, Pagination<AttendanceRecordResponse>>
{
    public async Task<Result<Pagination<AttendanceRecordResponse>>> Handle(GetAttendanceRecordsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new AttendanceRecordSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<AttendanceRecordResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<AttendanceRecordResponse>(
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
            return Result.Failure<Pagination<AttendanceRecordResponse>>(AttendanceRecordErrores.ErrorConsulta);
        }
    }
}
