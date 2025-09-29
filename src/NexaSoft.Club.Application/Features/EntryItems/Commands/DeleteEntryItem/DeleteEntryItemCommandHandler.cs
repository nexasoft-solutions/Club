using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.EntryItems;

namespace NexaSoft.Club.Application.Features.EntryItems.Commands.DeleteEntryItem;

public class DeleteEntryItemCommandHandler(
    IGenericRepository<EntryItem> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEntryItemCommandHandler> _logger
) : ICommandHandler<DeleteEntryItemCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEntryItemCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de EntryItem con ID {EntryItemId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("EntryItem con ID {EntryItemId} no encontrado", command.Id);
                return Result.Failure<bool>(EntryItemErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar EntryItem con ID {EntryItemId}", command.Id);
            return Result.Failure<bool>(EntryItemErrores.ErrorDelete);
        }
    }
}
