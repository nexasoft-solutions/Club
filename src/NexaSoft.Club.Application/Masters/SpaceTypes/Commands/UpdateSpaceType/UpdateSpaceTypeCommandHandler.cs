using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpaceTypes;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Commands.UpdateSpaceType;

public class UpdateSpaceTypeCommandHandler(
    IGenericRepository<SpaceType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSpaceTypeCommandHandler> _logger
) : ICommandHandler<UpdateSpaceTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSpaceTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de SpaceType con ID {SpaceTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SpaceType con ID {SpaceTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(SpaceTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Name,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SpaceType con ID {SpaceTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar SpaceType con ID {SpaceTypeId}", command.Id);
            return Result.Failure<bool>(SpaceTypeErrores.ErrorEdit);
        }
    }
}
