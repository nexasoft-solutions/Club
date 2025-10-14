using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SourceModules;

namespace NexaSoft.Club.Application.Masters.SourceModules.Commands.UpdateSourceModule;

public class UpdateSourceModuleCommandHandler(
    IGenericRepository<SourceModule> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSourceModuleCommandHandler> _logger
) : ICommandHandler<UpdateSourceModuleCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSourceModuleCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de SourceModule con ID {SourceModuleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SourceModule con ID {SourceModuleId} no encontrado", command.Id);
                return Result.Failure<bool>(SourceModuleErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
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
            _logger.LogInformation("SourceModule con ID {SourceModuleId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar SourceModule con ID {SourceModuleId}", command.Id);
            return Result.Failure<bool>(SourceModuleErrores.ErrorEdit);
        }
    }
}
