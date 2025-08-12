using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.DeleteEstudioAmbiental;

public class DeleteEstudioAmbientalCommandHandler(
    IGenericRepository<EstudioAmbiental> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEstudioAmbientalCommandHandler> _logger
) : ICommandHandler<DeleteEstudioAmbientalCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEstudioAmbientalCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de EstudioAmbiental con ID {EstudioAmbientalId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("EstudioAmbiental con ID {EstudioAmbientalId} no encontrado", command.Id);
            return Result.Failure<bool>(EstudioAmbientalErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime());

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
            _logger.LogError(ex, "Error al eliminar EstudioAmbiental con ID {EstudioAmbientalId}", command.Id);
            return Result.Failure<bool>(EstudioAmbientalErrores.ErrorDelete);
        }
    }
}
