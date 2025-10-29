using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Expenses;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Commands.UpdateExpense;

public class UpdateExpenseCommandHandler(
    IGenericRepository<Expense> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateExpenseCommandHandler> _logger
) : ICommandHandler<UpdateExpenseCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateExpenseCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Expense con ID {ExpenseId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Expense con ID {ExpenseId} no encontrado", command.Id);
                return Result.Failure<bool>(ExpenseErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.CostCenterId,
            command.Description,
            command.ExpenseDate,
            command.Amount,
            command.DocumentNumber,
            command.DocumentPath,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Expense con ID {ExpenseId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Expense con ID {ExpenseId}", command.Id);
            return Result.Failure<bool>(ExpenseErrores.ErrorEdit);
        }
    }
}
