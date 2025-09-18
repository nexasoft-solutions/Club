using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.UpdateSubCapitulo;

public class UpdateSubCapituloCommandHandler(
    IGenericRepository<SubCapitulo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSubCapituloCommandHandler> _logger
) : ICommandHandler<UpdateSubCapituloCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSubCapituloCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de SubCapitulo con ID {SubCapituloId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("SubCapitulo con ID {SubCapituloId} no encontrado", command.Id);
            return Result.Failure<bool>(SubCapituloErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.NombreSubCapitulo,
            command.DescripcionSubCapitulo,
            command.CapituloId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SubCapitulo con ID {SubCapituloId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar SubCapitulo con ID {SubCapituloId}", command.Id);
            return Result.Failure<bool>(SubCapituloErrores.ErrorEdit);
        }
    }
}
