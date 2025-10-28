using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.DeleteEmployeeInfo;

public class DeleteEmployeeInfoCommandHandler(
    IGenericRepository<EmployeeInfo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEmployeeInfoCommandHandler> _logger
) : ICommandHandler<DeleteEmployeeInfoCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEmployeeInfoCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de EmployeeInfo con ID {EmployeeInfoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("EmployeeInfo con ID {EmployeeInfoId} no encontrado", command.Id);
                return Result.Failure<bool>(EmployeeInfoErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar EmployeeInfo con ID {EmployeeInfoId}", command.Id);
            return Result.Failure<bool>(EmployeeInfoErrores.ErrorDelete);
        }
    }
}
