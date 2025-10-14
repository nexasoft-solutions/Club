using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountTypes;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Commands.DeleteAccountType;

public class DeleteAccountTypeCommandHandler(
    IGenericRepository<AccountType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteAccountTypeCommandHandler> _logger
) : ICommandHandler<DeleteAccountTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAccountTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de AccountType con ID {AccountTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("AccountType con ID {AccountTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(AccountTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar AccountType con ID {AccountTypeId}", command.Id);
            return Result.Failure<bool>(AccountTypeErrores.ErrorDelete);
        }
    }
}
