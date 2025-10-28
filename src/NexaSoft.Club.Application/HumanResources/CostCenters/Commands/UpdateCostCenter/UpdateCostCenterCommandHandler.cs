using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Commands.UpdateCostCenter;

public class UpdateCostCenterCommandHandler(
    IGenericRepository<CostCenter> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateCostCenterCommandHandler> _logger
) : ICommandHandler<UpdateCostCenterCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateCostCenterCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de CostCenter con ID {CostCenterId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("CostCenter con ID {CostCenterId} no encontrado", command.Id);
                return Result.Failure<bool>(CostCenterErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.ParentCostCenterId,
            command.CostCenterTypeId,
            command.Description,
            command.ResponsibleId,
            command.Budget,
            command.StartDate,
            command.EndDate,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("CostCenter con ID {CostCenterId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar CostCenter con ID {CostCenterId}", command.Id);
            return Result.Failure<bool>(CostCenterErrores.ErrorEdit);
        }
    }
}
