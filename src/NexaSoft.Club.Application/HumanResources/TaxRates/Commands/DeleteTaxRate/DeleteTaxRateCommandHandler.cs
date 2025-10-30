using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TaxRates;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Commands.DeleteTaxRate;

public class DeleteTaxRateCommandHandler(
    IGenericRepository<TaxRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteTaxRateCommandHandler> _logger
) : ICommandHandler<DeleteTaxRateCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteTaxRateCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de TaxRate con ID {TaxRateId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("TaxRate con ID {TaxRateId} no encontrado", command.Id);
                return Result.Failure<bool>(TaxRateErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar TaxRate con ID {TaxRateId}", command.Id);
            return Result.Failure<bool>(TaxRateErrores.ErrorDelete);
        }
    }
}
