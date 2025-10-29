using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.DeletePayrollFormula;

public class DeletePayrollFormulaCommandHandler(
    IGenericRepository<PayrollFormula> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollFormulaCommandHandler> _logger
) : ICommandHandler<DeletePayrollFormulaCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollFormulaCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollFormula con ID {PayrollFormulaId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollFormula con ID {PayrollFormulaId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollFormulaErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollFormula con ID {PayrollFormulaId}", command.Id);
            return Result.Failure<bool>(PayrollFormulaErrores.ErrorDelete);
        }
    }
}
