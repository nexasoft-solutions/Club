using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.DeleteCumplimiento;

public class DeleteCumplimientoCommandHandler(
    IGenericRepository<Cumplimiento> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteCumplimientoCommandHandler> _logger
) : ICommandHandler<DeleteCumplimientoCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCumplimientoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Cumplimiento con ID {CumplimientoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Cumplimiento con ID {CumplimientoId} no encontrado", command.Id);
            return Result.Failure<bool>(CumplimientoErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Cumplimiento con ID {CumplimientoId}", command.Id);
            return Result.Failure<bool>(CumplimientoErrores.ErrorDelete);
        }
    }
}
