using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Commands.UpdateConceptCalculationType;

public class UpdateConceptCalculationTypeCommandHandler(
    IGenericRepository<ConceptCalculationType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateConceptCalculationTypeCommandHandler> _logger
) : ICommandHandler<UpdateConceptCalculationTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateConceptCalculationTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de ConceptCalculationType con ID {ConceptCalculationTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ConceptCalculationType con ID {ConceptCalculationTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(ConceptCalculationTypeErrores.NoEncontrado);
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
            _logger.LogInformation("ConceptCalculationType con ID {ConceptCalculationTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar ConceptCalculationType con ID {ConceptCalculationTypeId}", command.Id);
            return Result.Failure<bool>(ConceptCalculationTypeErrores.ErrorEdit);
        }
    }
}
