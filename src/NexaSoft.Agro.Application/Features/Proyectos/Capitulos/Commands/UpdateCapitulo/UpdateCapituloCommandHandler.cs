using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.UpdateCapitulo;

public class UpdateCapituloCommandHandler(
    IGenericRepository<Capitulo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateCapituloCommandHandler> _logger
) : ICommandHandler<UpdateCapituloCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateCapituloCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Capitulo con ID {CapituloId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Capitulo con ID {CapituloId} no encontrado", command.Id);
            return Result.Failure<bool>(CapituloErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.NombreCapitulo,
            command.DescripcionCapitulo,
            command.EstudioAmbientalId,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Capitulo con ID {CapituloId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Capitulo con ID {CapituloId}", command.Id);
            return Result.Failure<bool>(CapituloErrores.ErrorEdit);
        }
    }
}
