using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.DeleteColaborador;

public class DeleteColaboradorCommandHandler(
    IGenericRepository<Colaborador> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteColaboradorCommandHandler> _logger
) : ICommandHandler<DeleteColaboradorCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteColaboradorCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Colaborador con ID {ColaboradorId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Colaborador con ID {ColaboradorId} no encontrado", command.Id);
            return Result.Failure<bool>(ColaboradorErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Colaborador con ID {ColaboradorId}", command.Id);
            return Result.Failure<bool>(ColaboradorErrores.ErrorDelete);
        }
    }
}
