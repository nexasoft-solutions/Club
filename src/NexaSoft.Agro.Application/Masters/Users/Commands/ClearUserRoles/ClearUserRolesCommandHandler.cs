using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.ClearUserRoles;

public class ClearUserRolesCommandHandler(
    IGenericRepository<User> _userRepository,
    IUserRoleRepository _userRolesRepository,
    IUnitOfWork _unitOfWork,
    ILogger<ClearUserRolesCommandHandler> logger)
    : ICommandHandler<ClearUserRolesCommand, bool>
{
    public async Task<Result<bool>> Handle(ClearUserRolesCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            return Result.Failure<bool>(UserErrores.NoEncontrado);

        await _userRolesRepository.ClearForUserAsync(command.UserId,cancellationToken);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            logger.LogInformation("Todos los roles fueron removidos del usuario {UserId}", user.Id);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            logger.LogError(ex, "Error al limpiar roles del usuario");
            return Result.Failure<bool>(UserErrores.ErrorLimpiarRoles);
        }
    }
}
