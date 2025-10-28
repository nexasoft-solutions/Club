using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.UpdateTimeRequestType;

public class UpdateTimeRequestTypeCommandHandler(
    IGenericRepository<TimeRequestType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateTimeRequestTypeCommandHandler> _logger
) : ICommandHandler<UpdateTimeRequestTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateTimeRequestTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de TimeRequestType con ID {TimeRequestTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("TimeRequestType con ID {TimeRequestTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(TimeRequestTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
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
            _logger.LogInformation("TimeRequestType con ID {TimeRequestTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar TimeRequestType con ID {TimeRequestTypeId}", command.Id);
            return Result.Failure<bool>(TimeRequestTypeErrores.ErrorEdit);
        }
    }
}
