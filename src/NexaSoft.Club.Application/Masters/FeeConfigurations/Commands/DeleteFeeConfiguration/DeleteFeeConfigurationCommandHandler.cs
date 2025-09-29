using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Commands.DeleteFeeConfiguration;

public class DeleteFeeConfigurationCommandHandler(
    IGenericRepository<FeeConfiguration> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteFeeConfigurationCommandHandler> _logger
) : ICommandHandler<DeleteFeeConfigurationCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteFeeConfigurationCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de FeeConfiguration con ID {FeeConfigurationId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("FeeConfiguration con ID {FeeConfigurationId} no encontrado", command.Id);
                return Result.Failure<bool>(FeeConfigurationErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar FeeConfiguration con ID {FeeConfigurationId}", command.Id);
            return Result.Failure<bool>(FeeConfigurationErrores.ErrorDelete);
        }
    }
}
