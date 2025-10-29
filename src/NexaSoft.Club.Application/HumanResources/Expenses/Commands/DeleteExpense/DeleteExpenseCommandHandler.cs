using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Expenses;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Commands.DeleteExpense;

public class DeleteExpenseCommandHandler(
    IGenericRepository<Expense> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteExpenseCommandHandler> _logger
) : ICommandHandler<DeleteExpenseCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Expense con ID {ExpenseId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Expense con ID {ExpenseId} no encontrado", command.Id);
                return Result.Failure<bool>(ExpenseErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Expense con ID {ExpenseId}", command.Id);
            return Result.Failure<bool>(ExpenseErrores.ErrorDelete);
        }
    }
}
