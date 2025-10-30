using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.DeletePayrollPeriodStatus;

public class DeletePayrollPeriodStatusCommandHandler(
    IGenericRepository<PayrollPeriodStatus> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollPeriodStatusCommandHandler> _logger
) : ICommandHandler<DeletePayrollPeriodStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollPeriodStatusCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollPeriodStatus con ID {PayrollPeriodStatusId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollPeriodStatus con ID {PayrollPeriodStatusId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollPeriodStatusErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollPeriodStatus con ID {PayrollPeriodStatusId}", command.Id);
            return Result.Failure<bool>(PayrollPeriodStatusErrores.ErrorDelete);
        }
    }
}
