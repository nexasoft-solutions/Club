using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.CreateEmployeeInfo;

public class CreateEmployeeInfoCommandHandler(
    IGenericRepository<EmployeeInfo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateEmployeeInfoCommandHandler> _logger
) : ICommandHandler<CreateEmployeeInfoCommand, long>
{
  public async Task<Result<long>> Handle(CreateEmployeeInfoCommand command, CancellationToken cancellationToken)
  {

    _logger.LogInformation("Iniciando proceso de creación de EmployeeInfo");

    bool existsEmployeeCode = await _repository.ExistsAsync(c => c.EmployeeCode == command.EmployeeCode, cancellationToken);
    if (existsEmployeeCode)
    {
      return Result.Failure<long>(EmployeeInfoErrores.Duplicado);
    }

    bool existsBankAccountNumber = await _repository.ExistsAsync(c => c.BankAccountNumber == command.BankAccountNumber, cancellationToken);
    if (existsBankAccountNumber)
    {
      return Result.Failure<long>(EmployeeInfoErrores.Duplicado);
    }

    bool existsCci_Number = await _repository.ExistsAsync(c => c.CciNumber == command.CciNumber, cancellationToken);
    if (existsCci_Number)
    {
      return Result.Failure<long>(EmployeeInfoErrores.Duplicado);
    }

    var entity = EmployeeInfo.Create(
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
        command.CciNumber,
        (int)EstadosEnum.Activo,
        _dateTimeProvider.CurrentTime.ToUniversalTime(),
        command.CreatedBy
    );

    try
    {
      await _unitOfWork.BeginTransactionAsync(cancellationToken);
      await _repository.AddAsync(entity, cancellationToken);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
      await _unitOfWork.CommitAsync(cancellationToken);
      _logger.LogInformation("EmployeeInfo con ID {EmployeeInfoId} creado satisfactoriamente", entity.Id);

      return Result.Success(entity.Id);
    }
    catch (Exception ex)
    {
      await _unitOfWork.RollbackAsync(cancellationToken);
      _logger.LogError(ex, "Error al crear EmployeeInfo");
      return Result.Failure<long>(EmployeeInfoErrores.ErrorSave);
    }
  }
}
