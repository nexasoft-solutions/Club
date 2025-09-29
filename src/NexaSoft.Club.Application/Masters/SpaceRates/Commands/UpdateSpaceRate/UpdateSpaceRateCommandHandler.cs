using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Commands.UpdateSpaceRate;

public class UpdateSpaceRateCommandHandler(
    IGenericRepository<SpaceRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSpaceRateCommandHandler> _logger
) : ICommandHandler<UpdateSpaceRateCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSpaceRateCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de SpaceRate con ID {SpaceRateId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SpaceRate con ID {SpaceRateId} no encontrado", command.Id);
                return Result.Failure<bool>(SpaceRateErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.SpaceId,
            command.MemberTypeId,
            command.Rate,
            command.IsActive,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SpaceRate con ID {SpaceRateId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar SpaceRate con ID {SpaceRateId}", command.Id);
            return Result.Failure<bool>(SpaceRateErrores.ErrorEdit);
        }
    }
}
