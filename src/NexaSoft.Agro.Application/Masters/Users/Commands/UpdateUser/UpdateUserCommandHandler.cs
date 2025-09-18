using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    IGenericRepository<User> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateUserCommandHandler> _logger
) : ICommandHandler<UpdateUserCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de User con ID {UserId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("User con ID {UserId} no encontrado", command.Id);
            return Result.Failure<bool>(UserErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.UserApellidos,
            command.UserNombres,   
            command.Password,
            command.Email,
            command.UserDni,
            command.UserTelefono,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("User con ID {UserId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar User con ID {UserId}", command.Id);
            return Result.Failure<bool>(UserErrores.ErrorEdit);
        }
    }
}
