using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.UpdatePlano;

public class UpdatePlanoCommandHandler(
    IGenericRepository<Plano> _repository,
    IGenericRepository<PlanoDetalle> _repositoryDetalle,
    IPlanoRepository _planoRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePlanoCommandHandler> _logger
) : ICommandHandler<UpdatePlanoCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePlanoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Plano con ID {PlanoId}", command.Id);

        var entity = await _planoRepository.GetPlanoByIdDetalle(command.Id, cancellationToken);
        if (entity.Value is null)
        {
            _logger.LogWarning("Plano con ID {PlanoId} no encontrado", command.Id);
            return Result.Failure<bool>(PlanoErrores.NoEncontrado);
        }

        entity.Value.Update(
            command.Id,
            command.EscalaId,
            command.SistemaProyeccion,
            command.NombrePlano,
            command.CodigoPlano,
            command.ArchivoId,
            command.ColaboradorId,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        // 1. Eliminar los que ya no están
        var incomingIds = command.Detalles.Select(d => d.Id).ToHashSet();
        var toRemove = entity.Value.Detalles.Where(d => !incomingIds.Contains(d.Id)).ToList();
        foreach (var d in toRemove)
             await _repositoryDetalle.DeleteAsync(d);
            //_dbContext.Remove(d);

        // 2. Agregar o actualizar
        foreach (var dto in command.Detalles)
        {
            var existing = entity.Value.Detalles.FirstOrDefault(d => d.Id == dto.Id);
            if (existing is not null)
            {
                // Update                
                existing.Descripcion = dto.Descripcion;
                existing.Coordenadas = dto.Coordenadas;
                
            }
            else
            {
                var nuevo = PlanoDetalle.Create(
                    dto.planoId,
                    dto.Descripcion,
                    dto.Coordenadas,
                    _dateTimeProvider.CurrentTime.ToUniversalTime()
                );
                entity.Value.AgregarDetalle(nuevo);
            }
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Plano con ID {PlanoId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Plano con ID {PlanoId}", command.Id);
            return Result.Failure<bool>(PlanoErrores.ErrorEdit);
        }
    }
}
