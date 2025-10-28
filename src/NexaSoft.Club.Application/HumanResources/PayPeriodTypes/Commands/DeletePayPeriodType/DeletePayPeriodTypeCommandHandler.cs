using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.DeletePayPeriodType;

public class DeletePayPeriodTypeCommandHandler(
    IGenericRepository<PayPeriodType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayPeriodTypeCommandHandler> _logger
) : ICommandHandler<DeletePayPeriodTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayPeriodTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayPeriodType con ID {PayPeriodTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayPeriodType con ID {PayPeriodTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayPeriodTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayPeriodType con ID {PayPeriodTypeId}", command.Id);
            return Result.Failure<bool>(PayPeriodTypeErrores.ErrorDelete);
        }
    }
}
