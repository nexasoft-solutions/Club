using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Commands.DeleteCostCenterType;

public class DeleteCostCenterTypeCommandHandler(
    IGenericRepository<CostCenterType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteCostCenterTypeCommandHandler> _logger
) : ICommandHandler<DeleteCostCenterTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCostCenterTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de CostCenterType con ID {CostCenterTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("CostCenterType con ID {CostCenterTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(CostCenterTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar CostCenterType con ID {CostCenterTypeId}", command.Id);
            return Result.Failure<bool>(CostCenterTypeErrores.ErrorDelete);
        }
    }
}
