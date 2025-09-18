using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.DeleteEstructura;

public class DeleteEstructuraCommandHandler(
    IGenericRepository<Estructura> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEstructuraCommandHandler> _logger
) : ICommandHandler<DeleteEstructuraCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEstructuraCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Estructura con ID {EstructuraId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Estructura con ID {EstructuraId} no encontrado", command.Id);
            return Result.Failure<bool>(EstructuraErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.UsuarioEliminacion);

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
            _logger.LogError(ex, "Error al eliminar Estructura con ID {EstructuraId}", command.Id);
            return Result.Failure<bool>(EstructuraErrores.ErrorDelete);
        }
    }
}
