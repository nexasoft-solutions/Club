using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.DeletePaymentMethodType;

public class DeletePaymentMethodTypeCommandHandler(
    IGenericRepository<PaymentMethodType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePaymentMethodTypeCommandHandler> _logger
) : ICommandHandler<DeletePaymentMethodTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePaymentMethodTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PaymentMethodType con ID {PaymentMethodTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PaymentMethodType con ID {PaymentMethodTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PaymentMethodTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PaymentMethodType con ID {PaymentMethodTypeId}", command.Id);
            return Result.Failure<bool>(PaymentMethodTypeErrores.ErrorDelete);
        }
    }
}
