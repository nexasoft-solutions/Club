using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Positions;

namespace NexaSoft.Club.Application.HumanResources.Positions.Commands.UpdatePosition;

public class UpdatePositionCommandHandler(
    IGenericRepository<Position> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePositionCommandHandler> _logger
) : ICommandHandler<UpdatePositionCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Position con ID {PositionId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Position con ID {PositionId} no encontrado", command.Id);
                return Result.Failure<bool>(PositionErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.DepartmentId,
            command.EmployeeTypeId,
            command.BaseSalary,
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
            _logger.LogInformation("Position con ID {PositionId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Position con ID {PositionId}", command.Id);
            return Result.Failure<bool>(PositionErrores.ErrorEdit);
        }
    }
}
