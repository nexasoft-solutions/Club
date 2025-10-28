using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.DeleteEmployeeType;

public class DeleteEmployeeTypeCommandHandler(
    IGenericRepository<EmployeeType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEmployeeTypeCommandHandler> _logger
) : ICommandHandler<DeleteEmployeeTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEmployeeTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de EmployeeType con ID {EmployeeTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("EmployeeType con ID {EmployeeTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(EmployeeTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar EmployeeType con ID {EmployeeTypeId}", command.Id);
            return Result.Failure<bool>(EmployeeTypeErrores.ErrorDelete);
        }
    }
}
