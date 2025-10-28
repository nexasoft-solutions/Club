using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.UpdatePaymentMethodType;

public class UpdatePaymentMethodTypeCommandHandler(
    IGenericRepository<PaymentMethodType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePaymentMethodTypeCommandHandler> _logger
) : ICommandHandler<UpdatePaymentMethodTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePaymentMethodTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PaymentMethodType con ID {PaymentMethodTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PaymentMethodType con ID {PaymentMethodTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PaymentMethodTypeErrores.NoEncontrado);
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
            _logger.LogInformation("PaymentMethodType con ID {PaymentMethodTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PaymentMethodType con ID {PaymentMethodTypeId}", command.Id);
            return Result.Failure<bool>(PaymentMethodTypeErrores.ErrorEdit);
        }
    }
}
