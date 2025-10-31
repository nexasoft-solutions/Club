using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.UpdatePayrollType;

public class UpdatePayrollTypeCommandHandler(
    IGenericRepository<PayrollType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollTypeCommandHandler> _logger
) : ICommandHandler<UpdatePayrollTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollType con ID {PayrollTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollType con ID {PayrollTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollTypeErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("PayrollType con ID {PayrollTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollType con ID {PayrollTypeId}", command.Id);
            return Result.Failure<bool>(PayrollTypeErrores.ErrorEdit);
        }
    }
}
