using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.DeleteOrganizacion;

public class DeleteOrganizacionCommandHandler(
    IGenericRepository<Organizacion> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteOrganizacionCommandHandler> _logger
) : ICommandHandler<DeleteOrganizacionCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteOrganizacionCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Organizacion con ID {OrganizacionId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Organizacion con ID {OrganizacionId} no encontrado", command.Id);
            return Result.Failure<bool>(OrganizacionErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Organizacion con ID {OrganizacionId}", command.Id);
            return Result.Failure<bool>(OrganizacionErrores.ErrorDelete);
        }
    }
}
