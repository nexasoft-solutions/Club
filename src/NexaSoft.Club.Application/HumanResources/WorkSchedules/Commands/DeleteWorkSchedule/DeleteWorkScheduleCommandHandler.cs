using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.DeleteWorkSchedule;

public class DeleteWorkScheduleCommandHandler(
    IGenericRepository<WorkSchedule> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteWorkScheduleCommandHandler> _logger
) : ICommandHandler<DeleteWorkScheduleCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteWorkScheduleCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de WorkSchedule con ID {WorkScheduleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("WorkSchedule con ID {WorkScheduleId} no encontrado", command.Id);
                return Result.Failure<bool>(WorkScheduleErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar WorkSchedule con ID {WorkScheduleId}", command.Id);
            return Result.Failure<bool>(WorkScheduleErrores.ErrorDelete);
        }
    }
}
