using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.UpdateEstructura;

public class UpdateEstructuraCommandHandler(
    IGenericRepository<Estructura> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEstructuraCommandHandler> _logger
) : ICommandHandler<UpdateEstructuraCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEstructuraCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Estructura con ID {EstructuraId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Estructura con ID {EstructuraId} no encontrado", command.Id);
            return Result.Failure<bool>(EstructuraErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.TipoEstructuraId,
            command.NombreEstructura,
            command.DescripcionEstructura,
            command.PadreEstructuraId,
            command.SubCapituloId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Estructura con ID {EstructuraId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Estructura con ID {EstructuraId}", command.Id);
            return Result.Failure<bool>(EstructuraErrores.ErrorEdit);
        }
    }
}
