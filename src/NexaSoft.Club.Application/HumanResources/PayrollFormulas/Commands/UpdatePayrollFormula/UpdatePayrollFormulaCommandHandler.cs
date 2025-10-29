using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.UpdatePayrollFormula;

public class UpdatePayrollFormulaCommandHandler(
    IGenericRepository<PayrollFormula> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollFormulaCommandHandler> _logger
) : ICommandHandler<UpdatePayrollFormulaCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollFormulaCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollFormula con ID {PayrollFormulaId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollFormula con ID {PayrollFormulaId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollFormulaErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.FormulaExpression,
            command.Description,
            command.Variables,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollFormula con ID {PayrollFormulaId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollFormula con ID {PayrollFormulaId}", command.Id);
            return Result.Failure<bool>(PayrollFormulaErrores.ErrorEdit);
        }
    }
}
