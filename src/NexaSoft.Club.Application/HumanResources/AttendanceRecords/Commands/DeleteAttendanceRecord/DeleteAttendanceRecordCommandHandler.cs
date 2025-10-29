using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.DeleteAttendanceRecord;

public class DeleteAttendanceRecordCommandHandler(
    IGenericRepository<AttendanceRecord> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteAttendanceRecordCommandHandler> _logger
) : ICommandHandler<DeleteAttendanceRecordCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAttendanceRecordCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de AttendanceRecord con ID {AttendanceRecordId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("AttendanceRecord con ID {AttendanceRecordId} no encontrado", command.Id);
                return Result.Failure<bool>(AttendanceRecordErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.DeletedBy);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al eliminar AttendanceRecord con ID {AttendanceRecordId}", command.Id);
            return Result.Failure<bool>(AttendanceRecordErrores.ErrorDelete);
        }
    }
}
