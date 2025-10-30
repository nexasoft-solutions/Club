using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.DeletePayrollConceptEmployee;

public class DeletePayrollConceptEmployeeCommandHandler(
    IGenericRepository<PayrollConceptEmployee> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollConceptEmployeeCommandHandler> _logger
) : ICommandHandler<DeletePayrollConceptEmployeeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollConceptEmployeeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollConceptEmployee con ID {PayrollConceptEmployeeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConceptEmployee con ID {PayrollConceptEmployeeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConceptEmployeeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollConceptEmployee con ID {PayrollConceptEmployeeId}", command.Id);
            return Result.Failure<bool>(PayrollConceptEmployeeErrores.ErrorDelete);
        }
    }
}
