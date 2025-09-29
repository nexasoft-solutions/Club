using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.EntryItems;

namespace NexaSoft.Club.Application.Features.EntryItems.Commands.UpdateEntryItem;

public class UpdateEntryItemCommandHandler(
    IGenericRepository<EntryItem> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEntryItemCommandHandler> _logger
) : ICommandHandler<UpdateEntryItemCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEntryItemCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de EntryItem con ID {EntryItemId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("EntryItem con ID {EntryItemId} no encontrado", command.Id);
                return Result.Failure<bool>(EntryItemErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.EntryId,
            command.AccountId,
            command.DebitAmount,
            command.CreditAmount,
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
            _logger.LogInformation("EntryItem con ID {EntryItemId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar EntryItem con ID {EntryItemId}", command.Id);
            return Result.Failure<bool>(EntryItemErrores.ErrorEdit);
        }
    }
}
