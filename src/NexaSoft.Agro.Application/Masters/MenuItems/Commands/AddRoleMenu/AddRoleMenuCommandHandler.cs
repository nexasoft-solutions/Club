using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Menus;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.AddRoleMenu;

public class AddRoleMenuCommandHandler(
    IGenericRepository<MenuItemOption> _repository ,
    IGenericRepository<Role> _roleRepository,
    IUnitOfWork _unitOfWork,
    ILogger<AddRoleMenuCommandHandler> _logger
) : ICommandHandler<AddRoleMenuCommand, bool>
{
    public async Task<Result<bool>> Handle(AddRoleMenuCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Asignando roles al menú {MenuId}", command.MenuId);

        // 1. Verificar si el menú existe
        var menu = await _repository.GetByIdAsync(command.MenuId, cancellationToken);
        if (menu is null)
        {
            _logger.LogWarning("Menú no encontrado: {MenuId}", command.MenuId);
            return Result.Failure<bool>(MenuItemErrores.NoEncontrado);
        }

        // 2. Limpiar roles actuales (si se quiere sobrescribir)
        menu.Roles.Clear();

        int totalAgregados = 0;

        foreach (var roleId in command.RoleIds.Distinct())
        {
            // 3. Validar existencia del rol antes de agregar
            var exists = await _roleRepository.ExistsAsync(roleId, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Rol inválido: {RoleId}", roleId);
                continue;
            }

            menu.AddRole(roleId); // ← Aquí usás tu método encapsulado
            totalAgregados++;
        }

        if (totalAgregados == 0)
        {
            _logger.LogWarning("No se agregaron roles válidos.");
            return Result.Failure<bool>(MenuItemErrores.ErrorRolesNoAsignados);
        }

        // 4. Persistir cambios
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
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
