using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Commands.UpdateCostCenterType;

public class UpdateCostCenterTypeCommandHandler(
    IGenericRepository<CostCenterType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateCostCenterTypeCommandHandler> _logger
) : ICommandHandler<UpdateCostCenterTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateCostCenterTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de CostCenterType con ID {CostCenterTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("CostCenterType con ID {CostCenterTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(CostCenterTypeErrores.NoEncontrado);
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
            _logger.LogInformation("CostCenterType con ID {CostCenterTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar CostCenterType con ID {CostCenterTypeId}", command.Id);
            return Result.Failure<bool>(CostCenterTypeErrores.ErrorEdit);
        }
    }
}
