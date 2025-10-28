using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Departments;

namespace NexaSoft.Club.Application.HumanResources.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler(
    IGenericRepository<Department> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteDepartmentCommandHandler> _logger
) : ICommandHandler<DeleteDepartmentCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Department con ID {DepartmentId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Department con ID {DepartmentId} no encontrado", command.Id);
                return Result.Failure<bool>(DepartmentErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Department con ID {DepartmentId}", command.Id);
            return Result.Failure<bool>(DepartmentErrores.ErrorDelete);
        }
    }
}
