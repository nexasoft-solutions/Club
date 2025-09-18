using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.DeleteSubCapitulo;

public class DeleteSubCapituloCommandHandler(
    IGenericRepository<SubCapitulo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSubCapituloCommandHandler> _logger
) : ICommandHandler<DeleteSubCapituloCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSubCapituloCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de SubCapitulo con ID {SubCapituloId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("SubCapitulo con ID {SubCapituloId} no encontrado", command.Id);
            return Result.Failure<bool>(SubCapituloErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(), command.UsuarioEliminacion);

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
            _logger.LogError(ex, "Error al eliminar SubCapitulo con ID {SubCapituloId}", command.Id);
            return Result.Failure<bool>(SubCapituloErrores.ErrorDelete);
        }
    }
}
