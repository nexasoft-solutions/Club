using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Menus;
using NexaSoft.Club.Domain.Masters.Roles;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.MenuItems.Commands.AddRoleMenu;

public class AddRoleMenuCommandHandler(
    IGenericRepository<MenuItemOption> _repository,
    IGenericRepository<Role> _roleRepository,
    IUnitOfWork _unitOfWork,
    ILogger<AddRoleMenuCommandHandler> _logger
) : ICommandHandler<AddRoleMenuCommand, bool>
{
    public async Task<Result<bool>> Handle(AddRoleMenuCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Asignando roles al menú {MenuId}", command.MenuId);

        var spec = new MenuWithRolesSpec(command.MenuId);
        var menu = await _repository.GetEntityWithSpec(spec, cancellationToken);

        if (menu is null)
        {
            _logger.LogWarning("Menú no encontrado: {MenuId}", command.MenuId);
            return Result.Failure<bool>(MenuItemErrores.NoEncontrado);
        }

        // 2. Iniciar transacción antes de cualquier persistencia
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Eliminar explícitamente los roles existentes en la base de datos
            menu.Roles.Clear();
            await _unitOfWork.SaveChangesAsync(cancellationToken); // Persistir la eliminación antes de agregar nuevos

            int totalAgregados = 0;
            foreach (var roleId in command.RoleIds.Distinct())
            {
                // Validar existencia del rol antes de agregar
                var exists = await _roleRepository.ExistsAsync(roleId, cancellationToken);
                if (!exists)
                {
                    _logger.LogWarning("Rol inválido: {RoleId}", roleId);
                    continue;
                }

                menu.AddRole(roleId);
                totalAgregados++;
            }

            if (totalAgregados == 0)
            {
                _logger.LogWarning("No se agregaron roles válidos.");
                await _unitOfWork.RollbackAsync(cancellationToken);
                return Result.Failure<bool>(MenuItemErrores.ErrorRolesNoAsignados);
            }

            // Persistir cambios finales

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Roles asignados correctamente al menú: {MenuId}", command.MenuId);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al guardar roles en el menú.");
            return Result.Failure<bool>(MenuItemErrores.ErrorSave);
        }
    }
}
