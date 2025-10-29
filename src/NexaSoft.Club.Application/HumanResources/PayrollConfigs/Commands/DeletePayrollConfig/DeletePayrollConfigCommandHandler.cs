using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.DeletePayrollConfig;

public class DeletePayrollConfigCommandHandler(
    IGenericRepository<PayrollConfig> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollConfigCommandHandler> _logger
) : ICommandHandler<DeletePayrollConfigCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollConfigCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollConfig con ID {PayrollConfigId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConfig con ID {PayrollConfigId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConfigErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollConfig con ID {PayrollConfigId}", command.Id);
            return Result.Failure<bool>(PayrollConfigErrores.ErrorDelete);
        }
    }
}
