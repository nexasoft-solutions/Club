using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.CreatePayrollConceptEmployee;

public class CreatePayrollConceptEmployeeCommandHandler(
    IGenericRepository<PayrollConceptEmployee> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollConceptEmployeeCommandHandler> _logger
) : ICommandHandler<CreatePayrollConceptEmployeeCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollConceptEmployeeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollConceptEmployee");

        var entity = PayrollConceptEmployee.Create(
            command.PayrollConceptId,
            command.EmployeeId,
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
            _logger.LogInformation("PayrollConceptEmployee con ID {PayrollConceptEmployeeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollConceptEmployee");
            return Result.Failure<long>(PayrollConceptEmployeeErrores.ErrorSave);
        }
    }
}
