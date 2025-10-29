using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.RateTypes;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Commands.UpdateRateType;

public class UpdateRateTypeCommandHandler(
    IGenericRepository<RateType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateRateTypeCommandHandler> _logger
) : ICommandHandler<UpdateRateTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateRateTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de RateType con ID {RateTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("RateType con ID {RateTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(RateTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
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
            _logger.LogInformation("RateType con ID {RateTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar RateType con ID {RateTypeId}", command.Id);
            return Result.Failure<bool>(RateTypeErrores.ErrorEdit);
        }
    }
}
