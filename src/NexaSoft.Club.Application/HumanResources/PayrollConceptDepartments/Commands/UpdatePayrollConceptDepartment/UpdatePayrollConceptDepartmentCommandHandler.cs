using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.UpdatePayrollConceptDepartment;

public class UpdatePayrollConceptDepartmentCommandHandler(
    IGenericRepository<PayrollConceptDepartment> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollConceptDepartmentCommandHandler> _logger
) : ICommandHandler<UpdatePayrollConceptDepartmentCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollConceptDepartmentCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollConceptDepartment con ID {PayrollConceptDepartmentId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConceptDepartment con ID {PayrollConceptDepartmentId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConceptDepartmentErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.PayrollConceptId,
            command.DepartmentId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollConceptDepartment con ID {PayrollConceptDepartmentId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollConceptDepartment con ID {PayrollConceptDepartmentId}", command.Id);
            return Result.Failure<bool>(PayrollConceptDepartmentErrores.ErrorEdit);
        }
    }
}
