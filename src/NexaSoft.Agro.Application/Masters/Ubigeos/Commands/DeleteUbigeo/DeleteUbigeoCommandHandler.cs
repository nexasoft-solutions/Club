using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Ubigeos;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Commands.DeleteUbigeo;

public class DeleteUbigeoCommandHandler(
    IGenericRepository<Ubigeo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteUbigeoCommandHandler> _logger
) : ICommandHandler<DeleteUbigeoCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteUbigeoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Ubigeo con ID {UbigeoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Ubigeo con ID {UbigeoId} no encontrado", command.Id);
            return Result.Failure<bool>(UbigeoErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Ubigeo con ID {UbigeoId}", command.Id);
            return Result.Failure<bool>(UbigeoErrores.ErrorDelete);
        }
    }
}
