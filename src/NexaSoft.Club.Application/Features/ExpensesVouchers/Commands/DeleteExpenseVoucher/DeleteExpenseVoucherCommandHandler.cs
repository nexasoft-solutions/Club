using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.DeleteExpenseVoucher;

public class DeleteExpenseVoucherCommandHandler(
    IGenericRepository<ExpenseVoucher> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteExpenseVoucherCommandHandler> _logger
) : ICommandHandler<DeleteExpenseVoucherCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteExpenseVoucherCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de ExpenseVoucher con ID {ExpenseVoucherId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("ExpenseVoucher con ID {ExpenseVoucherId} no encontrado", command.Id);
            return Result.Failure<bool>(ExpenseVoucherErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(), command.DeletedBy);

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
            _logger.LogError(ex, "Error al eliminar ExpenseVoucher con ID {ExpenseVoucherId}", command.Id);
            return Result.Failure<bool>(ExpenseVoucherErrores.ErrorDelete);
        }
    }
}
