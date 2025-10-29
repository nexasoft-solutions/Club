using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.SpecialRates;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.DeleteSpecialRate;

public class DeleteSpecialRateCommandHandler(
    IGenericRepository<SpecialRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSpecialRateCommandHandler> _logger
) : ICommandHandler<DeleteSpecialRateCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSpecialRateCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de SpecialRate con ID {SpecialRateId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SpecialRate con ID {SpecialRateId} no encontrado", command.Id);
                return Result.Failure<bool>(SpecialRateErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar SpecialRate con ID {SpecialRateId}", command.Id);
            return Result.Failure<bool>(SpecialRateErrores.ErrorDelete);
        }
    }
}
