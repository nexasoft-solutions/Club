using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

public class CreatePayrollPeriodCommandHandler(
    IGenericRepository<PayrollPeriod> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollPeriodCommandHandler> _logger
) : ICommandHandler<CreatePayrollPeriodCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollPeriodCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollPeriod");

     bool existsPeriodName = await _repository.ExistsAsync(c => c.PeriodName == command.PeriodName, cancellationToken);
     if (existsPeriodName)
     {
       return Result.Failure<long>(PayrollPeriodErrores.Duplicado);
     }

        var entity = PayrollPeriod.Create(
            command.PeriodName,
            command.StartDate,
            command.EndDate,
            command.TotalAmount,
            command.TotalEmployees,
            command.StatusId,
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
            _logger.LogInformation("PayrollPeriod con ID {PayrollPeriodId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollPeriod");
            return Result.Failure<long>(PayrollPeriodErrores.ErrorSave);
        }
    }
}
