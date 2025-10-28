using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.UpdateBankAccountType;

public class UpdateBankAccountTypeCommandHandler(
    IGenericRepository<BankAccountType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateBankAccountTypeCommandHandler> _logger
) : ICommandHandler<UpdateBankAccountTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateBankAccountTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de BankAccountType con ID {BankAccountTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("BankAccountType con ID {BankAccountTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(BankAccountTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
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
            _logger.LogInformation("BankAccountType con ID {BankAccountTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar BankAccountType con ID {BankAccountTypeId}", command.Id);
            return Result.Failure<bool>(BankAccountTypeErrores.ErrorEdit);
        }
    }
}
