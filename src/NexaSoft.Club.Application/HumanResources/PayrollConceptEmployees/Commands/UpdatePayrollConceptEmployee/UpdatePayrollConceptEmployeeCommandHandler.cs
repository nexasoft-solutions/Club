using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.UpdatePayrollConceptEmployee;

public class UpdatePayrollConceptEmployeeCommandHandler(
    IGenericRepository<PayrollConceptEmployee> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollConceptEmployeeCommandHandler> _logger
) : ICommandHandler<UpdatePayrollConceptEmployeeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollConceptEmployeeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollConceptEmployee con ID {PayrollConceptEmployeeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConceptEmployee con ID {PayrollConceptEmployeeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConceptEmployeeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.PayrollConceptId,
            command.EmployeeId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollConceptEmployee con ID {PayrollConceptEmployeeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollConceptEmployee con ID {PayrollConceptEmployeeId}", command.Id);
            return Result.Failure<bool>(PayrollConceptEmployeeErrores.ErrorEdit);
        }
    }
}
