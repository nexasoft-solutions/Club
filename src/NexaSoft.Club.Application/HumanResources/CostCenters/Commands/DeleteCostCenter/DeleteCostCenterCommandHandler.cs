using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Commands.DeleteCostCenter;

public class DeleteCostCenterCommandHandler(
    IGenericRepository<CostCenter> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteCostCenterCommandHandler> _logger
) : ICommandHandler<DeleteCostCenterCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCostCenterCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de CostCenter con ID {CostCenterId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("CostCenter con ID {CostCenterId} no encontrado", command.Id);
                return Result.Failure<bool>(CostCenterErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar CostCenter con ID {CostCenterId}", command.Id);
            return Result.Failure<bool>(CostCenterErrores.ErrorDelete);
        }
    }
}
