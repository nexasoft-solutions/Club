using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.DeletePayrollConcept;

public class DeletePayrollConceptCommandHandler(
    IGenericRepository<PayrollConcept> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePayrollConceptCommandHandler> _logger
) : ICommandHandler<DeletePayrollConceptCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePayrollConceptCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de PayrollConcept con ID {PayrollConceptId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("PayrollConcept con ID {PayrollConceptId} no encontrado", command.Id);
                return Result.Failure<bool>(PayrollConceptErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar PayrollConcept con ID {PayrollConceptId}", command.Id);
            return Result.Failure<bool>(PayrollConceptErrores.ErrorDelete);
        }
    }
}
