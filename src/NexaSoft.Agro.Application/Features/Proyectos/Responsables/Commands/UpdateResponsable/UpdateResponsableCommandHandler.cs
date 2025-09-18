using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.UpdateResponsable;

public class UpdateResponsableCommandHandler(
    IGenericRepository<Responsable> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateResponsableCommandHandler> _logger
) : ICommandHandler<UpdateResponsableCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateResponsableCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Responsable con ID {ResponsableId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Responsable con ID {ResponsableId} no encontrado", command.Id);
            return Result.Failure<bool>(ResponsableErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.NombreResponsable,
            command.CargoResponsable,
            command.CorreoResponsable,
            command.TelefonoResponsable,
            command.Observaciones,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Responsable con ID {ResponsableId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Responsable con ID {ResponsableId}", command.Id);
            return Result.Failure<bool>(ResponsableErrores.ErrorEdit);
        }
    }
}
