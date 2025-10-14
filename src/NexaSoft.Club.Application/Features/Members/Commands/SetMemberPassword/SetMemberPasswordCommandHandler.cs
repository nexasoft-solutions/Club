
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Users;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Club.Application.Features.Members.Commands.SetMemberPassword;

public class SetMemberPasswordCommandHandler(
    IGenericRepository<User> _userRepository,
    IUnitOfWork _unitOfWork,
    ILogger<SetMemberPasswordCommandHandler> _logger
) : ICommandHandler<SetMemberPasswordCommand, bool>
{
    public async Task<Result<bool>> Handle(
        SetMemberPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user == null)
            return Result.Failure<bool>(UserErrores.NoEncontrado);

        // Validar password (mínimo 6 caracteres)
        if (command.Password.Length < 6)
            return Result.Failure<bool>(UserErrores.PasswordErrado);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {


            user.SetPassword(BC.HashPassword(command.Password));

            if (!string.IsNullOrEmpty(command.BiometricToken))
                user.SetBiometricToken(command.BiometricToken);

            user.RecordLogin(command.DeviceId);

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Password configurado para user {UserId}", command.UserId);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear User");
            return Result.Failure<bool>(UserErrores.ErrorSave);
        }
    }
}