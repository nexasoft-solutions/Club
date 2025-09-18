using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.UpdateCumplimiento;

public class UpdateCumplimientoCommandHandler(
    IGenericRepository<Cumplimiento> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateCumplimientoCommandHandler> _logger
) : ICommandHandler<UpdateCumplimientoCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateCumplimientoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Cumplimiento con ID {CumplimientoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Cumplimiento con ID {CumplimientoId} no encontrado", command.Id);
            return Result.Failure<bool>(CumplimientoErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.FechaCumplimiento,
            command.RegistradoaTiempo,
            command.Observaciones,
            command.EventoRegulatorioId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Cumplimiento con ID {CumplimientoId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Cumplimiento con ID {CumplimientoId}", command.Id);
            return Result.Failure<bool>(CumplimientoErrores.ErrorEdit);
        }
    }
}
