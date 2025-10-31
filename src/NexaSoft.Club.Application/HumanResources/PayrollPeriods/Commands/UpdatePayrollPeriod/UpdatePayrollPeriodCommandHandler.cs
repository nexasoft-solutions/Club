using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.UpdatePayrollPeriod;

public class UpdatePayrollPeriodCommandHandler(
    IGenericRepository<PayrollPeriod> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollPeriodCommandHandler> _logger
) : ICommandHandler<UpdatePayrollPeriodCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollPeriod con ID {PayrollPeriodId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollPeriod con ID {PayrollPeriodId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollPeriodErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.PeriodName,
            command.PayrollTypeId,
            command.StartDate,
            command.EndDate,
            command.TotalAmount,
            command.TotalEmployees,
            command.StatusId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollPeriod con ID {PayrollPeriodId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollPeriod con ID {PayrollPeriodId}", command.Id);
            return Result.Failure<bool>(PayrollPeriodErrores.ErrorEdit);
        }
    }
}
