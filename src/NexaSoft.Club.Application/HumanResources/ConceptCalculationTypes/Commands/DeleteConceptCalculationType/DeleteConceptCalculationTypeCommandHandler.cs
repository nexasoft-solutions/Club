using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Commands.DeleteConceptCalculationType;

public class DeleteConceptCalculationTypeCommandHandler(
    IGenericRepository<ConceptCalculationType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteConceptCalculationTypeCommandHandler> _logger
) : ICommandHandler<DeleteConceptCalculationTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteConceptCalculationTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de ConceptCalculationType con ID {ConceptCalculationTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ConceptCalculationType con ID {ConceptCalculationTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(ConceptCalculationTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar ConceptCalculationType con ID {ConceptCalculationTypeId}", command.Id);
            return Result.Failure<bool>(ConceptCalculationTypeErrores.ErrorDelete);
        }
    }
}
