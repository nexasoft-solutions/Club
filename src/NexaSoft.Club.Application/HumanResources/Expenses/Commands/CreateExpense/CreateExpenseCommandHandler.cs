using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Expenses;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Commands.CreateExpense;

public class CreateExpenseCommandHandler(
    IGenericRepository<Expense> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateExpenseCommandHandler> _logger
) : ICommandHandler<CreateExpenseCommand, long>
{
    public async Task<Result<long>> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Expense");

     bool existsDescription = await _repository.ExistsAsync(c => c.Description == command.Description, cancellationToken);
     if (existsDescription)
     {
       return Result.Failure<long>(ExpenseErrores.Duplicado);
     }

     bool existsDocumentNumber = await _repository.ExistsAsync(c => c.DocumentNumber == command.DocumentNumber, cancellationToken);
     if (existsDocumentNumber)
     {
       return Result.Failure<long>(ExpenseErrores.Duplicado);
     }

     bool existsDocumentPath = await _repository.ExistsAsync(c => c.DocumentPath == command.DocumentPath, cancellationToken);
     if (existsDocumentPath)
     {
       return Result.Failure<long>(ExpenseErrores.Duplicado);
     }

        var entity = Expense.Create(
            command.CostCenterId,
            command.Description,
            command.ExpenseDate,
            command.Amount,
            command.DocumentNumber,
            command.DocumentPath,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Expense con ID {ExpenseId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Expense");
            return Result.Failure<long>(ExpenseErrores.ErrorSave);
        }
    }
}
