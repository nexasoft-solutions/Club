using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.DeletePayrollType;

public class DeletePayrollTypeCommandHandler(
    IGenericRepository<PayrollType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollTypeCommandHandler> _logger
) : ICommandHandler<DeletePayrollTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollType con ID {PayrollTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollType con ID {PayrollTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollTypeErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.DeletedBy);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al eliminar PayrollType con ID {PayrollTypeId}", command.Id);
            return Result.Failure<bool>(PayrollTypeErrores.ErrorDelete);
        }
    }
}
