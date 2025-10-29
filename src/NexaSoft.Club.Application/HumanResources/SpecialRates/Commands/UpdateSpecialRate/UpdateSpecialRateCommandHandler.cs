using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.SpecialRates;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.UpdateSpecialRate;

public class UpdateSpecialRateCommandHandler(
    IGenericRepository<SpecialRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSpecialRateCommandHandler> _logger
) : ICommandHandler<UpdateSpecialRateCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSpecialRateCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de SpecialRate con ID {SpecialRateId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SpecialRate con ID {SpecialRateId} no encontrado", command.Id);
                return Result.Failure<bool>(SpecialRateErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.RateTypeId,
            command.Name,
            command.Multiplier,
            command.StartTime,
            command.EndTime,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SpecialRate con ID {SpecialRateId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar SpecialRate con ID {SpecialRateId}", command.Id);
            return Result.Failure<bool>(SpecialRateErrores.ErrorEdit);
        }
    }
}
