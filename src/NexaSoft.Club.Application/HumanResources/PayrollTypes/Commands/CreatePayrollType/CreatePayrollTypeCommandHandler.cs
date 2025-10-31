using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.CreatePayrollType;

public class CreatePayrollTypeCommandHandler(
    IGenericRepository<PayrollType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreatePayrollTypeCommandHandler> _logger
) : ICommandHandler<CreatePayrollTypeCommand, long>
{
    public async Task<Result<long>> Handle(CreatePayrollTypeCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de PayrollType");

     bool existsCode = await _repository.ExistsAsync(c => c.Code == command.Code, cancellationToken);
     if (existsCode)
     {
       return Result.Failure<long>(PayrollTypeErrores.Duplicado);
     }

     bool existsName = await _repository.ExistsAsync(c => c.Name == command.Name, cancellationToken);
     if (existsName)
     {
       return Result.Failure<long>(PayrollTypeErrores.Duplicado);
     }

        var entity = PayrollType.Create(
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
            _logger.LogInformation("PayrollType con ID {PayrollTypeId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear PayrollType");
            return Result.Failure<long>(PayrollTypeErrores.ErrorSave);
        }
    }
}
