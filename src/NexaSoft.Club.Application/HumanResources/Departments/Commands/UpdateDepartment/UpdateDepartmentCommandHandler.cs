using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Departments;

namespace NexaSoft.Club.Application.HumanResources.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler(
    IGenericRepository<Department> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateDepartmentCommandHandler> _logger
) : ICommandHandler<UpdateDepartmentCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de Department con ID {DepartmentId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Department con ID {DepartmentId} no encontrado", command.Id);
                return Result.Failure<bool>(DepartmentErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.ParentDepartmentId,
            command.Description,
            command.ManagerId,
            command.CostCenterId,
            command.Location,
            command.PhoneExtension,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Department con ID {DepartmentId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar Department con ID {DepartmentId}", command.Id);
            return Result.Failure<bool>(DepartmentErrores.ErrorEdit);
        }
    }
}
