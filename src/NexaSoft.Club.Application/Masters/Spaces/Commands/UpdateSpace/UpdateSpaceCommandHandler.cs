using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.UpdateSpace;

public class UpdateSpaceCommandHandler(
    IGenericRepository<Space> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSpaceCommandHandler> _logger
) : ICommandHandler<UpdateSpaceCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSpaceCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Space con ID {SpaceId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Space con ID {SpaceId} no encontrado", command.Id);
                return Result.Failure<bool>(SpaceErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.SpaceName,
            command.SpaceType,
            command.Capacity,
            command.Description,
            command.StandardRate,
            command.IsActive,
            command.RequiresApproval,
            command.MaxReservationHours,
            command.IncomeAccountId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Space con ID {SpaceId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Space con ID {SpaceId}", command.Id);
            return Result.Failure<bool>(SpaceErrores.ErrorEdit);
        }
    }
}
