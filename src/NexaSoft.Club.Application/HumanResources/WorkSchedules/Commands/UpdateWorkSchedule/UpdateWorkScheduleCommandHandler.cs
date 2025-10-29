using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.UpdateWorkSchedule;

public class UpdateWorkScheduleCommandHandler(
    IGenericRepository<WorkSchedule> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateWorkScheduleCommandHandler> _logger
) : ICommandHandler<UpdateWorkScheduleCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateWorkScheduleCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de WorkSchedule con ID {WorkScheduleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("WorkSchedule con ID {WorkScheduleId} no encontrado", command.Id);
                return Result.Failure<bool>(WorkScheduleErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.EmployeeId,
            command.DayOfWeek,
            command.StartTime,
            command.EndTime,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("WorkSchedule con ID {WorkScheduleId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar WorkSchedule con ID {WorkScheduleId}", command.Id);
            return Result.Failure<bool>(WorkScheduleErrores.ErrorEdit);
        }
    }
}
