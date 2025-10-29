using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.UpdatePayrollConfig;

public class UpdatePayrollConfigCommandHandler(
    IGenericRepository<PayrollConfig> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollConfigCommandHandler> _logger
) : ICommandHandler<UpdatePayrollConfigCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollConfigCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollConfig con ID {PayrollConfigId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConfig con ID {PayrollConfigId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConfigErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.CompanyId,
            command.PayPeriodTypeId,
            command.RegularHoursPerDay,
            command.WorkDaysPerWeek,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollConfig con ID {PayrollConfigId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollConfig con ID {PayrollConfigId}", command.Id);
            return Result.Failure<bool>(PayrollConfigErrores.ErrorEdit);
        }
    }
}
