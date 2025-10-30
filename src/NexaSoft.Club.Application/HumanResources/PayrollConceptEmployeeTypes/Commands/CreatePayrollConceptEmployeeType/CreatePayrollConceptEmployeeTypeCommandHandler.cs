using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.CreatePayrollConceptEmployeeType;

public class CreatePayrollConceptEmployeeTypeCommandHandler(
    IGenericRepository<PayrollConceptEmployeeType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollConceptEmployeeTypeCommandHandler> _logger
) : ICommandHandler<CreatePayrollConceptEmployeeTypeCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollConceptEmployeeTypeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollConceptEmployeeType");

        var entity = PayrollConceptEmployeeType.Create(
            command.PayrollConceptId,
            command.EmployeeTypeId,
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
            _logger.LogInformation("PayrollConceptEmployeeType con ID {PayrollConceptEmployeeTypeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollConceptEmployeeType");
            return Result.Failure<long>(PayrollConceptEmployeeTypeErrores.ErrorSave);
        }
    }
}
