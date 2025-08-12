
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Roles;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.AssignRolesToUser;

public class AssignRolesToUserCommandHandler(
    IGenericRepository<User> _userRepository,
    IUserRoleRepository _userRolesRepository,
    IUnitOfWork _unitOfWork,
    ILogger<AssignRolesToUserCommandHandler> _logger
) : ICommandHandler<AssignRolesToUserCommand, bool>
{
    public async Task<Result<bool>> Handle(AssignRolesToUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Asignando roles al usuario: {UserId}", command.UserId);
  
        if (!await _userRepository.ExistsAsync(command.UserId, cancellationToken))
        {
            _logger.LogWarning("Ususario no encontrado: {UserId}", command.UserId);
            return Result.Failure<bool>(UserErrores.NoEncontrado);
        }

        // 2. Obtener y eliminar relaciones existentes
        var total = await _userRolesRepository.AddRangeAsync(
            command.UserId,
            command.RoleDefs,
            cancellationToken);

        if (total == 0)
        {
            _logger.LogWarning("No se encontraron roles para asignar");
            return Result.Failure<bool>(RoleErrores.ErrorRolesNoEncontrados);
        }
     
       
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Roles asignados al usuario correctamente: {UserId}", command.UserId);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al asignar roles al usuario.");
            return Result.Failure<bool>(UserErrores.ErrorSave);
        }
    }
}
