using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.DeleteEventoRegulatorio;

public class DeleteEventoRegulatorioCommandHandler(
    IGenericRepository<EventoRegulatorio> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEventoRegulatorioCommandHandler> _logger
) : ICommandHandler<DeleteEventoRegulatorioCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEventoRegulatorioCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de EventoRegulatorio con ID {EventoRegulatorioId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("EventoRegulatorio con ID {EventoRegulatorioId} no encontrado", command.Id);
            return Result.Failure<bool>(EventoRegulatorioErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar EventoRegulatorio con ID {EventoRegulatorioId}", command.Id);
            return Result.Failure<bool>(EventoRegulatorioErrores.ErrorDelete);
        }
    }
}
