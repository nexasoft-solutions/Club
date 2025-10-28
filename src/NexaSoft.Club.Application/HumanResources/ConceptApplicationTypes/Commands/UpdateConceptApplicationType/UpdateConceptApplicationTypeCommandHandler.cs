using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.UpdateConceptApplicationType;

public class UpdateConceptApplicationTypeCommandHandler(
    IGenericRepository<ConceptApplicationType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateConceptApplicationTypeCommandHandler> _logger
) : ICommandHandler<UpdateConceptApplicationTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateConceptApplicationTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de ConceptApplicationType con ID {ConceptApplicationTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ConceptApplicationType con ID {ConceptApplicationTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(ConceptApplicationTypeErrores.NoEncontrado);
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
            _logger.LogInformation("ConceptApplicationType con ID {ConceptApplicationTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar ConceptApplicationType con ID {ConceptApplicationTypeId}", command.Id);
            return Result.Failure<bool>(ConceptApplicationTypeErrores.ErrorEdit);
        }
    }
}
