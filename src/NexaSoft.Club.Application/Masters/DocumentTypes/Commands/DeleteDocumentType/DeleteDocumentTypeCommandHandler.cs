using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.DocumentTypes;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Commands.DeleteDocumentType;

public class DeleteDocumentTypeCommandHandler(
    IGenericRepository<DocumentType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteDocumentTypeCommandHandler> _logger
) : ICommandHandler<DeleteDocumentTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteDocumentTypeCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de DocumentType con ID {DocumentTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("DocumentType con ID {DocumentTypeId} no encontrado", command.Id);
                return Result.Failure<bool>(DocumentTypeErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar DocumentType con ID {DocumentTypeId}", command.Id);
            return Result.Failure<bool>(DocumentTypeErrores.ErrorDelete);
        }
    }
}
