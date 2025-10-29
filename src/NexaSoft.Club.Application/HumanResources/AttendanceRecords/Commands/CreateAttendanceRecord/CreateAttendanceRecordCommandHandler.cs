using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.CreateAttendanceRecord;

public class CreateAttendanceRecordCommandHandler(
    IGenericRepository<AttendanceRecord> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateAttendanceRecordCommandHandler> _logger
) : ICommandHandler<CreateAttendanceRecordCommand, long>
{
    public async Task<Result<long>> Handle(CreateAttendanceRecordCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de AttendanceRecord");

        var entity = AttendanceRecord.Create(
            command.EmployeeId,
            command.CostCenterId,
            command.RecordDate,
            command.CheckInTime,
            command.CheckOutTime,
            command.TotalHours,
            command.RegularHours,
            command.OvertimeHours,
            command.SundayHours,
            command.HolidayHours,
            command.NightHours,
            command.LateMinutes,
            command.EarlyDepartureMinutes,
            command.AttendanceStatusTypeId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("AttendanceRecord con ID {AttendanceRecordId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear AttendanceRecord");
            return Result.Failure<long>(AttendanceRecordErrores.ErrorSave);
        }
    }
}
