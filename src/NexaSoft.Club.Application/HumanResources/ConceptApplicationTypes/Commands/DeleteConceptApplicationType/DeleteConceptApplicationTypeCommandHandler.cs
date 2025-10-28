using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.DeleteConceptApplicationType;

public class DeleteConceptApplicationTypeCommandHandler(
    IGenericRepository<ConceptApplicationType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteConceptApplicationTypeCommandHandler> _logger
) : ICommandHandler<DeleteConceptApplicationTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteConceptApplicationTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de ConceptApplicationType con ID {ConceptApplicationTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("ConceptApplicationType con ID {ConceptApplicationTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(ConceptApplicationTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar ConceptApplicationType con ID {ConceptApplicationTypeId}", command.Id);
            return Result.Failure<bool>(ConceptApplicationTypeErrores.ErrorDelete);
        }
    }
}
