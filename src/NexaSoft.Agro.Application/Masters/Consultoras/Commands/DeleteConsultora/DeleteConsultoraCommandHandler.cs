using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Consultoras;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Commands.DeleteConsultora;

public class DeleteConsultoraCommandHandler(
    IGenericRepository<Consultora> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteConsultoraCommandHandler> _logger
) : ICommandHandler<DeleteConsultoraCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteConsultoraCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Consultora con ID {ConsultoraId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Consultora con ID {ConsultoraId} no encontrado", command.Id);
                return Result.Failure<bool>(ConsultoraErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime());

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
            _logger.LogError(ex,"Error al eliminar Consultora con ID {ConsultoraId}", command.Id);
            return Result.Failure<bool>(ConsultoraErrores.ErrorDelete);
        }
    }
}
