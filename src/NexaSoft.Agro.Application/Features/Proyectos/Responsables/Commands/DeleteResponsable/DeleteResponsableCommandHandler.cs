using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.DeleteResponsable;

public class DeleteResponsableCommandHandler(
    IGenericRepository<Responsable> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteResponsableCommandHandler> _logger
) : ICommandHandler<DeleteResponsableCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteResponsableCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Responsable con ID {ResponsableId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Responsable con ID {ResponsableId} no encontrado", command.Id);
            return Result.Failure<bool>(ResponsableErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Responsable con ID {ResponsableId}", command.Id);
            return Result.Failure<bool>(ResponsableErrores.ErrorDelete);
        }
    }
}
