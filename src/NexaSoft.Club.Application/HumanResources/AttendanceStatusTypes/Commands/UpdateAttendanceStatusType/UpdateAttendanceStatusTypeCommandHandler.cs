using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.UpdateAttendanceStatusType;

public class UpdateAttendanceStatusTypeCommandHandler(
    IGenericRepository<AttendanceStatusType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateAttendanceStatusTypeCommandHandler> _logger
) : ICommandHandler<UpdateAttendanceStatusTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateAttendanceStatusTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de AttendanceStatusType con ID {AttendanceStatusTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("AttendanceStatusType con ID {AttendanceStatusTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(AttendanceStatusTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.IsPaid,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("AttendanceStatusType con ID {AttendanceStatusTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar AttendanceStatusType con ID {AttendanceStatusTypeId}", command.Id);
            return Result.Failure<bool>(AttendanceStatusTypeErrores.ErrorEdit);
        }
    }
}
