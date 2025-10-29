using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.RateTypes;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Commands.DeleteRateType;

public class DeleteRateTypeCommandHandler(
    IGenericRepository<RateType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteRateTypeCommandHandler> _logger
) : ICommandHandler<DeleteRateTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteRateTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de RateType con ID {RateTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("RateType con ID {RateTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(RateTypeErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.DeletedBy);

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
            _logger.LogError(ex,"Error al eliminar RateType con ID {RateTypeId}", command.Id);
            return Result.Failure<bool>(RateTypeErrores.ErrorDelete);
        }
    }
}
