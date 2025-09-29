using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.UpdateExpenseVoucher;

public class UpdateExpenseVoucherCommandHandler(
    IGenericRepository<ExpenseVoucher> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateExpenseVoucherCommandHandler> _logger
) : ICommandHandler<UpdateExpenseVoucherCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateExpenseVoucherCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de ExpenseVoucher con ID {ExpenseVoucherId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("ExpenseVoucher con ID {ExpenseVoucherId} no encontrado", command.Id);
            return Result.Failure<bool>(ExpenseVoucherErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.EntryId!,
            command.VoucherNumber,
            command.SupplierName,
            command.Amount,
            command.IssueDate,
            command.Description,
            command.ExpenseAccountId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("ExpenseVoucher con ID {ExpenseVoucherId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar ExpenseVoucher con ID {ExpenseVoucherId}", command.Id);
            return Result.Failure<bool>(ExpenseVoucherErrores.ErrorEdit);
        }
    }
}
