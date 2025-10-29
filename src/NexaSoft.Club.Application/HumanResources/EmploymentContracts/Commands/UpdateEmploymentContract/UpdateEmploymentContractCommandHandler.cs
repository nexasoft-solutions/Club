using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.UpdateEmploymentContract;

public class UpdateEmploymentContractCommandHandler(
    IGenericRepository<EmploymentContract> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEmploymentContractCommandHandler> _logger
) : ICommandHandler<UpdateEmploymentContractCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEmploymentContractCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de EmploymentContract con ID {EmploymentContractId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("EmploymentContract con ID {EmploymentContractId} no encontrado", command.Id);
                return Result.Failure<bool>(EmploymentContractErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.EmployeeId,
            command.ContractTypeId,
            command.StartDate,
            command.EndDate,
            command.Salary,
            command.WorkingHours,
            command.DocumentPath,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EmploymentContract con ID {EmploymentContractId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar EmploymentContract con ID {EmploymentContractId}", command.Id);
            return Result.Failure<bool>(EmploymentContractErrores.ErrorEdit);
        }
    }
}
