using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TaxRates;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Commands.UpdateTaxRate;

public class UpdateTaxRateCommandHandler(
    IGenericRepository<TaxRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateTaxRateCommandHandler> _logger
) : ICommandHandler<UpdateTaxRateCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateTaxRateCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de TaxRate con ID {TaxRateId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("TaxRate con ID {TaxRateId} no encontrado", command.Id);
                return Result.Failure<bool>(TaxRateErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.RateValue,
            command.RateType,
            command.MinAmount,
            command.MaxAmount,
            command.EffectiveDate,
            command.EndDate,
            command.Category,
            command.Description,
            command.AppliesTo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("TaxRate con ID {TaxRateId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar TaxRate con ID {TaxRateId}", command.Id);
            return Result.Failure<bool>(TaxRateErrores.ErrorEdit);
        }
    }
}
