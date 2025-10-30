using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.UpdateLegalParameter;

public class UpdateLegalParameterCommandHandler(
    IGenericRepository<LegalParameter> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateLegalParameterCommandHandler> _logger
) : ICommandHandler<UpdateLegalParameterCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateLegalParameterCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de LegalParameter con ID {LegalParameterId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("LegalParameter con ID {LegalParameterId} no encontrado", command.Id);
                return Result.Failure<bool>(LegalParameterErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Code,
            command.Name,
            command.Value,
            command.ValueText,
            command.EffectiveDate,
            command.EndDate,
            command.Category,
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
            _logger.LogInformation("LegalParameter con ID {LegalParameterId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar LegalParameter con ID {LegalParameterId}", command.Id);
            return Result.Failure<bool>(LegalParameterErrores.ErrorEdit);
        }
    }
}
