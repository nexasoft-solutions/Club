using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.DeleteConstante;

public class DeleteConstanteCommandHandler(
    IGenericRepository<Constante> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteConstanteCommandHandler> _logger
) : ICommandHandler<DeleteConstanteCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteConstanteCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Constante con ID {ConstanteId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Constante con ID {ConstanteId} no encontrado", command.Id);
            return Result.Failure<bool>(ConstanteErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Constante con ID {ConstanteId}", command.Id);
            return Result.Failure<bool>(ConstanteErrores.ErrorDelete);
        }
    }
}
