using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.DeleteTimeRequestType;

public class DeleteTimeRequestTypeCommandHandler(
    IGenericRepository<TimeRequestType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteTimeRequestTypeCommandHandler> _logger
) : ICommandHandler<DeleteTimeRequestTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteTimeRequestTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de TimeRequestType con ID {TimeRequestTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("TimeRequestType con ID {TimeRequestTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(TimeRequestTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar TimeRequestType con ID {TimeRequestTypeId}", command.Id);
            return Result.Failure<bool>(TimeRequestTypeErrores.ErrorDelete);
        }
    }
}
