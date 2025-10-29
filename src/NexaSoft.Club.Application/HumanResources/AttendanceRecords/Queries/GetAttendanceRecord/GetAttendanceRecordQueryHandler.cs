using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Queries.GetAttendanceRecord;

public class GetAttendanceRecordQueryHandler(
    IGenericRepository<AttendanceRecord> _repository
) : IQueryHandler<GetAttendanceRecordQuery, AttendanceRecordResponse>
{
    public async Task<Result<AttendanceRecordResponse>> Handle(GetAttendanceRecordQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new AttendanceRecordSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<AttendanceRecordResponse>(AttendanceRecordErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<AttendanceRecordResponse>(AttendanceRecordErrores.ErrorConsulta);
        }
    }
}
