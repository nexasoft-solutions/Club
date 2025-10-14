using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.UpdateAccountingEntry;

/*public class UpdateAccountingEntryCommandHandler(
    IGenericRepository<AccountingEntry> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateAccountingEntryCommandHandler> _logger
) : ICommandHandler<UpdateAccountingEntryCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateAccountingEntryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de AccountingEntry con ID {AccountingEntryId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("AccountingEntry con ID {AccountingEntryId} no encontrado", command.Id);
            return Result.Failure<bool>(AccountingEntryErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.EntryNumber,
            command.EntryDate,
            command.Description,
            command.TotalDebit,
            command.TotalCredit,
            command.IsAdjusted,
            command.AdjustmentReason,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("AccountingEntry con ID {AccountingEntryId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar AccountingEntry con ID {AccountingEntryId}", command.Id);
            return Result.Failure<bool>(AccountingEntryErrores.ErrorEdit);
        }
    }
}
*/