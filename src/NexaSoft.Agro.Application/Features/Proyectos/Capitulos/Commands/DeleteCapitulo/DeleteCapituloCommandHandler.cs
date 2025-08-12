using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.DeleteCapitulo;

public class DeleteCapituloCommandHandler(
    IGenericRepository<Capitulo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteCapituloCommandHandler> _logger
) : ICommandHandler<DeleteCapituloCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCapituloCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Capitulo con ID {CapituloId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Capitulo con ID {CapituloId} no encontrado", command.Id);
            return Result.Failure<bool>(CapituloErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Capitulo con ID {CapituloId}", command.Id);
            return Result.Failure<bool>(CapituloErrores.ErrorDelete);
        }
    }
}
