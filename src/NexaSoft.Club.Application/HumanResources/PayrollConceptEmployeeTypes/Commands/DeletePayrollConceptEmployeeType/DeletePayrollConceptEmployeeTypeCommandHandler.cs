using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.DeletePayrollConceptEmployeeType;

public class DeletePayrollConceptEmployeeTypeCommandHandler(
    IGenericRepository<PayrollConceptEmployeeType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollConceptEmployeeTypeCommandHandler> _logger
) : ICommandHandler<DeletePayrollConceptEmployeeTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollConceptEmployeeTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollConceptEmployeeType con ID {PayrollConceptEmployeeTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConceptEmployeeType con ID {PayrollConceptEmployeeTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConceptEmployeeTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollConceptEmployeeType con ID {PayrollConceptEmployeeTypeId}", command.Id);
            return Result.Failure<bool>(PayrollConceptEmployeeTypeErrores.ErrorDelete);
        }
    }
}
