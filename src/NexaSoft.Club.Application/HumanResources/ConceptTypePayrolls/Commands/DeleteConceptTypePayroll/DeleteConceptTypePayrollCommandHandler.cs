using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.DeleteConceptTypePayroll;

public class DeleteConceptTypePayrollCommandHandler(
    IGenericRepository<ConceptTypePayroll> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteConceptTypePayrollCommandHandler> _logger
) : ICommandHandler<DeleteConceptTypePayrollCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteConceptTypePayrollCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de ConceptTypePayroll con ID {ConceptTypePayrollId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ConceptTypePayroll con ID {ConceptTypePayrollId} no encontrado", command.Id);
                return Result.Failure<bool>(ConceptTypePayrollErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar ConceptTypePayroll con ID {ConceptTypePayrollId}", command.Id);
            return Result.Failure<bool>(ConceptTypePayrollErrores.ErrorDelete);
        }
    }
}
