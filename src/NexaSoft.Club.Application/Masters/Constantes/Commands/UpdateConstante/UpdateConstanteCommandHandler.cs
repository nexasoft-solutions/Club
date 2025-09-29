using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Constantes;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.UpdateConstante;

public class UpdateConstanteCommandHandler(
    IGenericRepository<Constante> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateConstanteCommandHandler> _logger
) : ICommandHandler<UpdateConstanteCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateConstanteCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Constante con ID {ConstanteId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Constante con ID {ConstanteId} no encontrado", command.Id);
            return Result.Failure<bool>(ConstanteErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.TipoConstante,
            command.Valor,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Constante con ID {ConstanteId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            var errores = ex.Message;
            _logger.LogError(ex, "Error al actualizar Constante con ID {ConstanteId}", command.Id);
            return Result.Failure<bool>(ConstanteErrores.ErrorEdit);
        }
    }
}
