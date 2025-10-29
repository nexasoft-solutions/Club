using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.DeletePayrollPeriod;

public class DeletePayrollPeriodCommandHandler(
    IGenericRepository<PayrollPeriod> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollPeriodCommandHandler> _logger
) : ICommandHandler<DeletePayrollPeriodCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollPeriodCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollPeriod con ID {PayrollPeriodId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollPeriod con ID {PayrollPeriodId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollPeriodErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollPeriod con ID {PayrollPeriodId}", command.Id);
            return Result.Failure<bool>(PayrollPeriodErrores.ErrorDelete);
        }
    }
}
