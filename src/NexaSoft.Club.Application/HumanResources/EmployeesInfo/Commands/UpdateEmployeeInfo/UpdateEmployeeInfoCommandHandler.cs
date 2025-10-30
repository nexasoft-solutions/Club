using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.UpdateEmployeeInfo;

public class UpdateEmployeeInfoCommandHandler(
    IGenericRepository<EmployeeInfo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEmployeeInfoCommandHandler> _logger
) : ICommandHandler<UpdateEmployeeInfoCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEmployeeInfoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de EmployeeInfo con ID {EmployeeInfoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("EmployeeInfo con ID {EmployeeInfoId} no encontrado", command.Id);
            return Result.Failure<bool>(EmployeeInfoErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.EmployeeCode,
            command.UserId,
            command.PositionId,
            command.EmployeeTypeId,
            command.DepartmentId,
            command.HireDate,
            command.BaseSalary,
            command.PaymentMethodId,
            command.BankId,
            command.BankAccountTypeId,
            command.CurrencyId,
            command.BankAccountNumber,
            command.Cci_Number,
            command.CompanyId,
            command.CostCenterId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EmployeeInfo con ID {EmployeeInfoId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar EmployeeInfo con ID {EmployeeInfoId}", command.Id);
            return Result.Failure<bool>(EmployeeInfoErrores.ErrorEdit);
        }
    }
}
