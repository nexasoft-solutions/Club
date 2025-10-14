using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Commands.UpdateAccountingChart;

public class UpdateAccountingChartCommandHandler(
    IGenericRepository<AccountingChart> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateAccountingChartCommandHandler> _logger
) : ICommandHandler<UpdateAccountingChartCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateAccountingChartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de AccountingChart con ID {AccountingChartId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("AccountingChart con ID {AccountingChartId} no encontrado", command.Id);
            return Result.Failure<bool>(AccountingChartErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.AccountCode,
            command.AccountName,
            command.AccountTypeId,
            command.ParentAccountId,
            command.Level,
            command.AllowsTransactions,
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
            _logger.LogInformation("AccountingChart con ID {AccountingChartId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar AccountingChart con ID {AccountingChartId}", command.Id);
            return Result.Failure<bool>(AccountingChartErrores.ErrorEdit);
        }
    }
}
