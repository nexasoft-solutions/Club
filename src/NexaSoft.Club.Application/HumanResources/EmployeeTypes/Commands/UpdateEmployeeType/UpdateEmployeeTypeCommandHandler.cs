using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.UpdateEmployeeType;

public class UpdateEmployeeTypeCommandHandler(
    IGenericRepository<EmployeeType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEmployeeTypeCommandHandler> _logger
) : ICommandHandler<UpdateEmployeeTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEmployeeTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de EmployeeType con ID {EmployeeTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("EmployeeType con ID {EmployeeTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(EmployeeTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.Description,
            command.BaseSalary,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EmployeeType con ID {EmployeeTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar EmployeeType con ID {EmployeeTypeId}", command.Id);
            return Result.Failure<bool>(EmployeeTypeErrores.ErrorEdit);
        }
    }
}
