using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountTypes;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Commands.UpdateAccountType;

public class UpdateAccountTypeCommandHandler(
    IGenericRepository<AccountType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateAccountTypeCommandHandler> _logger
) : ICommandHandler<UpdateAccountTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateAccountTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de AccountType con ID {AccountTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("AccountType con ID {AccountTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(AccountTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Name,
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
            _logger.LogInformation("AccountType con ID {AccountTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar AccountType con ID {AccountTypeId}", command.Id);
            return Result.Failure<bool>(AccountTypeErrores.ErrorEdit);
        }
    }
}
