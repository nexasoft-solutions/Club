using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Commands.UpdateUbigeo;

public class UpdateUbigeoCommandHandler(
    IGenericRepository<Ubigeo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateUbigeoCommandHandler> _logger
) : ICommandHandler<UpdateUbigeoCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateUbigeoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Ubigeo con ID {UbigeoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Ubigeo con ID {UbigeoId} no encontrado", command.Id);
            return Result.Failure<bool>(UbigeoErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.Description,
            command.Level,
            command.ParentId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UserModification
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Ubigeo con ID {UbigeoId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Ubigeo con ID {UbigeoId}", command.Id);
            return Result.Failure<bool>(UbigeoErrores.ErrorEdit);
        }
    }
}
