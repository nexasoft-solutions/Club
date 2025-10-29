using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.DeleteEmploymentContract;

public class DeleteEmploymentContractCommandHandler(
    IGenericRepository<EmploymentContract> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteEmploymentContractCommandHandler> _logger
) : ICommandHandler<DeleteEmploymentContractCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteEmploymentContractCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de EmploymentContract con ID {EmploymentContractId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("EmploymentContract con ID {EmploymentContractId} no encontrado", command.Id);
                return Result.Failure<bool>(EmploymentContractErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar EmploymentContract con ID {EmploymentContractId}", command.Id);
            return Result.Failure<bool>(EmploymentContractErrores.ErrorDelete);
        }
    }
}
