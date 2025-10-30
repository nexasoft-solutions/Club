using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.DeletePayrollConceptDepartment;

public class DeletePayrollConceptDepartmentCommandHandler(
    IGenericRepository<PayrollConceptDepartment> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollConceptDepartmentCommandHandler> _logger
) : ICommandHandler<DeletePayrollConceptDepartmentCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollConceptDepartmentCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollConceptDepartment con ID {PayrollConceptDepartmentId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConceptDepartment con ID {PayrollConceptDepartmentId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConceptDepartmentErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollConceptDepartment con ID {PayrollConceptDepartmentId}", command.Id);
            return Result.Failure<bool>(PayrollConceptDepartmentErrores.ErrorDelete);
        }
    }
}
