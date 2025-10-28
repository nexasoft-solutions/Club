using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Banks;

namespace NexaSoft.Club.Application.HumanResources.Banks.Commands.DeleteBank;

public class DeleteBankCommandHandler(
    IGenericRepository<Bank> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteBankCommandHandler> _logger
) : ICommandHandler<DeleteBankCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteBankCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Bank con ID {BankId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Bank con ID {BankId} no encontrado", command.Id);
                return Result.Failure<bool>(BankErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Bank con ID {BankId}", command.Id);
            return Result.Failure<bool>(BankErrores.ErrorDelete);
        }
    }
}
