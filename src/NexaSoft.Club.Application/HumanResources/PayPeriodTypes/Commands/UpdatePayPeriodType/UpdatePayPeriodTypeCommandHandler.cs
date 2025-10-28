using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.UpdatePayPeriodType;

public class UpdatePayPeriodTypeCommandHandler(
    IGenericRepository<PayPeriodType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayPeriodTypeCommandHandler> _logger
) : ICommandHandler<UpdatePayPeriodTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayPeriodTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayPeriodType con ID {PayPeriodTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayPeriodType con ID {PayPeriodTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayPeriodTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.Days,
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
            _logger.LogInformation("PayPeriodType con ID {PayPeriodTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayPeriodType con ID {PayPeriodTypeId}", command.Id);
            return Result.Failure<bool>(PayPeriodTypeErrores.ErrorEdit);
        }
    }
}
