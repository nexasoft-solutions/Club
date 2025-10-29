using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.UpdateAttendanceRecord;

public class UpdateAttendanceRecordCommandHandler(
    IGenericRepository<AttendanceRecord> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateAttendanceRecordCommandHandler> _logger
) : ICommandHandler<UpdateAttendanceRecordCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateAttendanceRecordCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de AttendanceRecord con ID {AttendanceRecordId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("AttendanceRecord con ID {AttendanceRecordId} no encontrado", command.Id);
                return Result.Failure<bool>(AttendanceRecordErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
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
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("AttendanceRecord con ID {AttendanceRecordId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar AttendanceRecord con ID {AttendanceRecordId}", command.Id);
            return Result.Failure<bool>(AttendanceRecordErrores.ErrorEdit);
        }
    }
}
