using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.CreatePayrollConceptDepartment;

public class CreatePayrollConceptDepartmentCommandHandler(
    IGenericRepository<PayrollConceptDepartment> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollConceptDepartmentCommandHandler> _logger
) : ICommandHandler<CreatePayrollConceptDepartmentCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollConceptDepartmentCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollConceptDepartment");

        var entity = PayrollConceptDepartment.Create(
            command.PayrollConceptId,
            command.DepartmentId,
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
            _logger.LogInformation("PayrollConceptDepartment con ID {PayrollConceptDepartmentId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollConceptDepartment");
            return Result.Failure<long>(PayrollConceptDepartmentErrores.ErrorSave);
        }
    }
}
