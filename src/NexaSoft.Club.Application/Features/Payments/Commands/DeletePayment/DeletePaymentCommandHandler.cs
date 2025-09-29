using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Application.Features.Payments.Commands.DeletePayment;

public class DeletePaymentCommandHandler(
    IGenericRepository<Payment> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePaymentCommandHandler> _logger
) : ICommandHandler<DeletePaymentCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePaymentCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Payment con ID {PaymentId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Payment con ID {PaymentId} no encontrado", command.Id);
                return Result.Failure<bool>(PaymentErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Payment con ID {PaymentId}", command.Id);
            return Result.Failure<bool>(PaymentErrores.ErrorDelete);
        }
    }
}
