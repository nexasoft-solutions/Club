using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Currencies;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Commands.DeleteCurrency;

public class DeleteCurrencyCommandHandler(
    IGenericRepository<Currency> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteCurrencyCommandHandler> _logger
) : ICommandHandler<DeleteCurrencyCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCurrencyCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Currency con ID {CurrencyId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Currency con ID {CurrencyId} no encontrado", command.Id);
                return Result.Failure<bool>(CurrencyErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Currency con ID {CurrencyId}", command.Id);
            return Result.Failure<bool>(CurrencyErrores.ErrorDelete);
        }
    }
}
