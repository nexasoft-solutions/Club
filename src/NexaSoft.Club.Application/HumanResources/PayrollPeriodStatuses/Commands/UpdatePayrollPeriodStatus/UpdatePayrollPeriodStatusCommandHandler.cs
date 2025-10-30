using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.UpdatePayrollPeriodStatus;

public class UpdatePayrollPeriodStatusCommandHandler(
    IGenericRepository<PayrollPeriodStatus> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollPeriodStatusCommandHandler> _logger
) : ICommandHandler<UpdatePayrollPeriodStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollPeriodStatusCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollPeriodStatus con ID {PayrollPeriodStatusId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollPeriodStatus con ID {PayrollPeriodStatusId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollPeriodStatusErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollPeriodStatus con ID {PayrollPeriodStatusId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollPeriodStatus con ID {PayrollPeriodStatusId}", command.Id);
            return Result.Failure<bool>(PayrollPeriodStatusErrores.ErrorEdit);
        }
    }
}
