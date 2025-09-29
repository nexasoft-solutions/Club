using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommandHandler(
    IGenericRepository<Payment> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePaymentCommandHandler> _logger
) : ICommandHandler<UpdatePaymentCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Payment con ID {PaymentId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Payment con ID {PaymentId} no encontrado", command.Id);
            return Result.Failure<bool>(PaymentErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.MemberId,
            //command.FeeId,
            command.Amount,
            command.PaymentDate,
            command.PaymentMethod,
            command.ReferenceNumber,
            command.ReceiptNumber,
            command.IsPartial,
            command.AccountingEntryId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Payment con ID {PaymentId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Payment con ID {PaymentId}", command.Id);
            return Result.Failure<bool>(PaymentErrores.ErrorEdit);
        }
    }
}
