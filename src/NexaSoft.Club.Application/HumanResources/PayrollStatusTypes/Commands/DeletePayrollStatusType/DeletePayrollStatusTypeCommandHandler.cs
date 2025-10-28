using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Commands.DeletePayrollStatusType;

public class DeletePayrollStatusTypeCommandHandler(
    IGenericRepository<PayrollStatusType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollStatusTypeCommandHandler> _logger
) : ICommandHandler<DeletePayrollStatusTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollStatusTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollStatusType con ID {PayrollStatusTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollStatusType con ID {PayrollStatusTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollStatusTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollStatusType con ID {PayrollStatusTypeId}", command.Id);
            return Result.Failure<bool>(PayrollStatusTypeErrores.ErrorDelete);
        }
    }
}
