using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.DeleteLegalParameter;

public class DeleteLegalParameterCommandHandler(
    IGenericRepository<LegalParameter> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteLegalParameterCommandHandler> _logger
) : ICommandHandler<DeleteLegalParameterCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteLegalParameterCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de LegalParameter con ID {LegalParameterId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("LegalParameter con ID {LegalParameterId} no encontrado", command.Id);
                return Result.Failure<bool>(LegalParameterErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar LegalParameter con ID {LegalParameterId}", command.Id);
            return Result.Failure<bool>(LegalParameterErrores.ErrorDelete);
        }
    }
}
