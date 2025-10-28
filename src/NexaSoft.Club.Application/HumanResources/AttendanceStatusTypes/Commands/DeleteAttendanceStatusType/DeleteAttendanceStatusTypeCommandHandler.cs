using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.DeleteAttendanceStatusType;

public class DeleteAttendanceStatusTypeCommandHandler(
    IGenericRepository<AttendanceStatusType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteAttendanceStatusTypeCommandHandler> _logger
) : ICommandHandler<DeleteAttendanceStatusTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAttendanceStatusTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de AttendanceStatusType con ID {AttendanceStatusTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("AttendanceStatusType con ID {AttendanceStatusTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(AttendanceStatusTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar AttendanceStatusType con ID {AttendanceStatusTypeId}", command.Id);
            return Result.Failure<bool>(AttendanceStatusTypeErrores.ErrorDelete);
        }
    }
}
