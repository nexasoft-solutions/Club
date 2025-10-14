using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.PaymentTypes;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Commands.DeletePaymentType;

public class DeletePaymentTypeCommandHandler(
    IGenericRepository<PaymentType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePaymentTypeCommandHandler> _logger
) : ICommandHandler<DeletePaymentTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePaymentTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PaymentType con ID {PaymentTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PaymentType con ID {PaymentTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PaymentTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PaymentType con ID {PaymentTypeId}", command.Id);
            return Result.Failure<bool>(PaymentTypeErrores.ErrorDelete);
        }
    }
}
