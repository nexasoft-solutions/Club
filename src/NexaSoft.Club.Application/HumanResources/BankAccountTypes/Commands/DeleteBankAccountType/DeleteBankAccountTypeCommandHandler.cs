using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.DeleteBankAccountType;

public class DeleteBankAccountTypeCommandHandler(
    IGenericRepository<BankAccountType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteBankAccountTypeCommandHandler> _logger
) : ICommandHandler<DeleteBankAccountTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteBankAccountTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de BankAccountType con ID {BankAccountTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("BankAccountType con ID {BankAccountTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(BankAccountTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar BankAccountType con ID {BankAccountTypeId}", command.Id);
            return Result.Failure<bool>(BankAccountTypeErrores.ErrorDelete);
        }
    }
}
