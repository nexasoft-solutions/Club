using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Banks;

namespace NexaSoft.Club.Application.HumanResources.Banks.Commands.UpdateBank;

public class UpdateBankCommandHandler(
    IGenericRepository<Bank> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateBankCommandHandler> _logger
) : ICommandHandler<UpdateBankCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateBankCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Bank con ID {BankId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Bank con ID {BankId} no encontrado", command.Id);
                return Result.Failure<bool>(BankErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.WebSite,
            command.Phone,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Bank con ID {BankId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Bank con ID {BankId}", command.Id);
            return Result.Failure<bool>(BankErrores.ErrorEdit);
        }
    }
}
