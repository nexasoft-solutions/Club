using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.CreateAccountingEntry;
/*
public class CreateAccountingEntryCommandHandler(
    IGenericRepository<AccountingEntry> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateAccountingEntryCommandHandler> _logger
) : ICommandHandler<CreateAccountingEntryCommand, long>
{
    public async Task<Result<long>> Handle(CreateAccountingEntryCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de AccountingEntry");

        bool existsEntryNumber = await _repository.ExistsAsync(c => c.EntryNumber == command.EntryNumber, cancellationToken);
        if (existsEntryNumber)
        {
            return Result.Failure<long>(AccountingEntryErrores.Duplicado);
        }

        var entity = AccountingEntry.Create(
            command.EntryNumber,
            command.EntryDate,
            command.Description,
            command.sourceModule,
            command.sourceId,
            command.TotalDebit,
            command.TotalCredit,
            command.isAdjusted,
            command.adjustmentReason,
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
            _logger.LogInformation("AccountingEntry con ID {AccountingEntryId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear AccountingEntry");
            return Result.Failure<long>(AccountingEntryErrores.ErrorSave);
        }
    }
}
*/