using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.DocumentTypes;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Commands.UpdateDocumentType;

public class UpdateDocumentTypeCommandHandler(
    IGenericRepository<DocumentType> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateDocumentTypeCommandHandler> _logger
) : ICommandHandler<UpdateDocumentTypeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateDocumentTypeCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de DocumentType con ID {DocumentTypeId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("DocumentType con ID {DocumentTypeId} no encontrado", command.Id);
            return Result.Failure<bool>(DocumentTypeErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.Name,
            command.Description,
            command.Serie,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("DocumentType con ID {DocumentTypeId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar DocumentType con ID {DocumentTypeId}", command.Id);
            return Result.Failure<bool>(DocumentTypeErrores.ErrorEdit);
        }
    }
}
