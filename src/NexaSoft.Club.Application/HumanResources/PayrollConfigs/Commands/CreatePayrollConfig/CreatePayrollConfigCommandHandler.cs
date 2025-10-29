using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.CreatePayrollConfig;

public class CreatePayrollConfigCommandHandler(
    IGenericRepository<PayrollConfig> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollConfigCommandHandler> _logger
) : ICommandHandler<CreatePayrollConfigCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollConfigCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollConfig");

     bool existsRegularHoursPerDay = await _repository.ExistsAsync(c => c.RegularHoursPerDay == command.RegularHoursPerDay, cancellationToken);
     if (existsRegularHoursPerDay)
     {
       return Result.Failure<long>(PayrollConfigErrores.Duplicado);
     }

        var entity = PayrollConfig.Create(
            command.CompanyId,
            command.PayPeriodTypeId,
            command.RegularHoursPerDay,
            command.WorkDaysPerWeek,
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
            _logger.LogInformation("PayrollConfig con ID {PayrollConfigId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollConfig");
            return Result.Failure<long>(PayrollConfigErrores.ErrorSave);
        }
    }
}
