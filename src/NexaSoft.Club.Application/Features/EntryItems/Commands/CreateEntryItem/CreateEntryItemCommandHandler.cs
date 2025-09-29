using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Exceptions;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.EntryItems;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.EntryItems.Commands.CreateEntryItem;

public class CreateEntryItemCommandHandler(
    IGenericRepository<EntryItem> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateEntryItemCommandHandler> _logger
) : ICommandHandler<CreateEntryItemCommand, long>
{
    public async Task<Result<long>> Handle(CreateEntryItemCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de EntryItem");

        var entity = EntryItem.Create(
            command.EntryId,
            command.AccountId,
            command.DebitAmount,
            command.CreditAmount,
            command.Description,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EntryItem con ID {EntryItemId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear EntryItem");
            return Result.Failure<long>(EntryItemErrores.ErrorSave);
        }
    }
}
