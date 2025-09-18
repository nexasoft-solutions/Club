using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.UpdateOrganizacion;

public class UpdateOrganizacionCommandHandler(
    IGenericRepository<Organizacion> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateOrganizacionCommandHandler> _logger
) : ICommandHandler<UpdateOrganizacionCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOrganizacionCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Organizacion con ID {OrganizacionId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Organizacion con ID {OrganizacionId} no encontrado", command.Id);
            return Result.Failure<bool>(OrganizacionErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.NombreOrganizacion,
            command.ContactoOrganizacion,
            command.TelefonoContacto,
            command.SectorId,
            command.RucOrganizacion,
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
            _logger.LogInformation("Organizacion con ID {OrganizacionId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Organizacion con ID {OrganizacionId}", command.Id);
            return Result.Failure<bool>(OrganizacionErrores.ErrorEdit);
        }
    }
}
