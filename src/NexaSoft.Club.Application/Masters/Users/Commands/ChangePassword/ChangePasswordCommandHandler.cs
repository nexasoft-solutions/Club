
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Users;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Club.Application.Masters.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IGenericRepository<User> _userRepository,
    IUnitOfWork _unitOfWork,
    ILogger<ChangePasswordCommandHandler> _logger
) : ICommandHandler<ChangePasswordCommand, bool>
{
    public async Task<Result<bool>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de Cambio de contraseña de User");


        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("Usuario con ID {UserId} no encontrado.", command.UserId);
            return Result.Failure<bool>(UserErrores.NoEncontrado);
        }

        string hashedPassword = BC.HashPassword(command.NewPassword);
        
        user.ChangePassword(hashedPassword);
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("User con ID {UserId} actualizo satisfactoriamente su contraseña ", command.UserId);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar contraseña de User con ID {UserId}", command.UserId);
            return Result.Failure<bool>(UserErrores.ErrorEdit);
        }

    }
}
