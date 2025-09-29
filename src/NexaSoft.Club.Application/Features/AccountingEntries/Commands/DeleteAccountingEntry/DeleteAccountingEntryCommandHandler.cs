using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.DeleteAccountingEntry;

public class DeleteAccountingEntryCommandHandler(
    IGenericRepository<AccountingEntry> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteAccountingEntryCommandHandler> _logger
) : ICommandHandler<DeleteAccountingEntryCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAccountingEntryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de AccountingEntry con ID {AccountingEntryId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("AccountingEntry con ID {AccountingEntryId} no encontrado", command.Id);
            return Result.Failure<bool>(AccountingEntryErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(), command.DeletedBy);

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
            _logger.LogError(ex, "Error al eliminar AccountingEntry con ID {AccountingEntryId}", command.Id);
            return Result.Failure<bool>(AccountingEntryErrores.ErrorDelete);
        }
    }
}
