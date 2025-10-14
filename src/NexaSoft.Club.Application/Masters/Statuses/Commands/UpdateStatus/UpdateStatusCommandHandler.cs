using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Statuses;

namespace NexaSoft.Club.Application.Masters.Statuses.Commands.UpdateStatus;

public class UpdateStatusCommandHandler(
    IGenericRepository<Status> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateStatusCommandHandler> _logger
) : ICommandHandler<UpdateStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Status con ID {StatusId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Status con ID {StatusId} no encontrado", command.Id);
                return Result.Failure<bool>(StatusErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Name,
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
            _logger.LogInformation("Status con ID {StatusId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Status con ID {StatusId}", command.Id);
            return Result.Failure<bool>(StatusErrores.ErrorEdit);
        }
    }
}
