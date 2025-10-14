using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.PaymentTypes;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Commands.UpdatePaymentType;

public class UpdatePaymentTypeCommandHandler(
    IGenericRepository<PaymentType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePaymentTypeCommandHandler> _logger
) : ICommandHandler<UpdatePaymentTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePaymentTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PaymentType con ID {PaymentTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PaymentType con ID {PaymentTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PaymentTypeErrores.NoEncontrado);
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
            _logger.LogInformation("PaymentType con ID {PaymentTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PaymentType con ID {PaymentTypeId}", command.Id);
            return Result.Failure<bool>(PaymentTypeErrores.ErrorEdit);
        }
    }
}
