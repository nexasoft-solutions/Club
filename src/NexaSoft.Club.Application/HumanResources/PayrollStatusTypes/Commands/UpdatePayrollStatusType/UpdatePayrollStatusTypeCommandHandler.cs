using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Commands.UpdatePayrollStatusType;

public class UpdatePayrollStatusTypeCommandHandler(
    IGenericRepository<PayrollStatusType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePayrollStatusTypeCommandHandler> _logger
) : ICommandHandler<UpdatePayrollStatusTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePayrollStatusTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de PayrollStatusType con ID {PayrollStatusTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollStatusType con ID {PayrollStatusTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollStatusTypeErrores.NoEncontrado);
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
            _logger.LogInformation("PayrollStatusType con ID {PayrollStatusTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar PayrollStatusType con ID {PayrollStatusTypeId}", command.Id);
            return Result.Failure<bool>(PayrollStatusTypeErrores.ErrorEdit);
        }
    }
}
