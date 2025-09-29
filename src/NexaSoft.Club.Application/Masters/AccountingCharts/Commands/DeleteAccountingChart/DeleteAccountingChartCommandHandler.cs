using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Commands.DeleteAccountingChart;

public class DeleteAccountingChartCommandHandler(
    IGenericRepository<AccountingChart> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteAccountingChartCommandHandler> _logger
) : ICommandHandler<DeleteAccountingChartCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAccountingChartCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de AccountingChart con ID {AccountingChartId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("AccountingChart con ID {AccountingChartId} no encontrado", command.Id);
                return Result.Failure<bool>(AccountingChartErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar AccountingChart con ID {AccountingChartId}", command.Id);
            return Result.Failure<bool>(AccountingChartErrores.ErrorDelete);
        }
    }
}
