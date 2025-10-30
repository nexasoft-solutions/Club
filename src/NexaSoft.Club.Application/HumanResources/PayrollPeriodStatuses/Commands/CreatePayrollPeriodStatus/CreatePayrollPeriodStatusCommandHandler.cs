using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.CreatePayrollPeriodStatus;

public class CreatePayrollPeriodStatusCommandHandler(
    IGenericRepository<PayrollPeriodStatus> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollPeriodStatusCommandHandler> _logger
) : ICommandHandler<CreatePayrollPeriodStatusCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollPeriodStatusCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollPeriodStatus");

     bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
     if (existsCode)
     {
       return Result.Failure<long>(PayrollPeriodStatusErrores.Duplicado);
     }

     bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
     if (existsName)
     {
       return Result.Failure<long>(PayrollPeriodStatusErrores.Duplicado);
     }

        var entity = PayrollPeriodStatus.Create(
            command.Code,
            command.Name,
            command.Description,
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
            _logger.LogInformation("PayrollPeriodStatus con ID {PayrollPeriodStatusId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollPeriodStatus");
            return Result.Failure<long>(PayrollPeriodStatusErrores.ErrorSave);
        }
    }
}
