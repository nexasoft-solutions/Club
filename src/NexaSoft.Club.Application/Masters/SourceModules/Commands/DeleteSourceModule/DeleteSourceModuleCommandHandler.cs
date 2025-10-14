using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SourceModules;

namespace NexaSoft.Club.Application.Masters.SourceModules.Commands.DeleteSourceModule;

public class DeleteSourceModuleCommandHandler(
    IGenericRepository<SourceModule> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSourceModuleCommandHandler> _logger
) : ICommandHandler<DeleteSourceModuleCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSourceModuleCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de SourceModule con ID {SourceModuleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SourceModule con ID {SourceModuleId} no encontrado", command.Id);
                return Result.Failure<bool>(SourceModuleErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar SourceModule con ID {SourceModuleId}", command.Id);
            return Result.Failure<bool>(SourceModuleErrores.ErrorDelete);
        }
    }
}
