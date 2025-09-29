using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.UpdateFeeConfiguration;

public class UpdateFeeConfigurationCommandHandler(
    IGenericRepository<FeeConfiguration> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateFeeConfigurationCommandHandler> _logger
) : ICommandHandler<UpdateFeeConfigurationCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateFeeConfigurationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de FeeConfiguration con ID {FeeConfigurationId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("FeeConfiguration con ID {FeeConfigurationId} no encontrado", command.Id);
            return Result.Failure<bool>(FeeConfigurationErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.FeeName,
            command.PeriodicityId,
            command.DueDay,
            command.DefaultAmount,
            command.IsVariable,
            command.Priority,
            command.AppliesToFamily,
            command.IncomeAccountId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("FeeConfiguration con ID {FeeConfigurationId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar FeeConfiguration con ID {FeeConfigurationId}", command.Id);
            return Result.Failure<bool>(FeeConfigurationErrores.ErrorEdit);
        }
    }
}
