
using Microsoft.EntityFrameworkCore;
using NexaSoft.Club.Application.Masters.MenuItems;
using NexaSoft.Club.Application.Masters.Users;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Infrastructure.Repositories;

public class MenuItemRepository(ApplicationDbContext _dbContext, IUserRoleRepository _userRoleRepository) : IMenuItemRepository
{
    public async Task<List<MenuItemResponse>> GetMenuByUser(long IdUser, long IdRole, CancellationToken cancellationToken)
    {

        var userRoles = await _userRoleRepository.GetRolesForUserAsync(IdUser, cancellationToken);
        //Console.WriteLine($"Roles encontrados para el usuario {Id}: {string.Join(", ", userRoles)}");

        // 2. Obtener todos los menú relacionados con esos roles (usando JOIN)
        /*var menus = await (
            from menu in _dbContext.Set<MenuItemOption>()
            join menuRole in _dbContext.Set<MenuRole>() on menu.Id equals menuRole.MenuItemOptionId
            where userRoles.Contains(menuRole.RoleId) && menu.FechaEliminacion == null
            select menu
        ).Distinct().ToListAsync(cancellationToken); // Usar Distinct por si un menú tiene varios roles

        // 3. Obtener todos los hijos de esos menús
        var menuIds = menus.Select(m => m.Id).ToList();

        var children = await _dbContext.Set<MenuItemOption>()
            .Where(m => m.FechaEliminacion == null && m.ParentId != null && menuIds.Contains(m.ParentId.Value))
            .ToListAsync(cancellationToken);

        // 4. Asignar los hijos manualmente
        foreach (var parent in menus)
        {
            parent.Children = children.Where(c => c.ParentId == parent.Id).ToList();
        }

        // 5. Filtrar solo los menús raíz
        var rootMenus = menus.Where(m => m.ParentId == null).ToList();

        // 6. Mapear al DTO
        return rootMenus.Select(MapToDto).ToList();*/


        /*var menus = await _dbContext.Set<MenuItemOption>()
            .Where(m => m.FechaEliminacion == null &&
                        //m.Roles.Any(r => userRoles.Contains(r.RoleId)))
                        m.Roles.Any(r => r.RoleId == IdRole))
            .Include(m => m.Roles) 
            .Include(m => m.Children)
            .ToListAsync(cancellationToken);

        var rootMenus = menus.Where(m => m.ParentId == null).ToList();
        return rootMenus.Select(menu => MapToDto(menu)).ToList();*/

        var menus = await _dbContext.Set<MenuItemOption>()
            .Where(m => m.DeletedAt == null &&
                        m.Roles.Any(r => r.RoleId == IdRole))
            .Include(m => m.Roles)
            .ToListAsync(cancellationToken);

        // Filtrar solo hijos válidos también
        foreach (var menu in menus)
        {
            if (menu.Children != null)
            {
                var toRemove = menu.Children
                    .Where(child => !child.Roles.Any(r => r.RoleId == IdRole))
                    .ToList();

                foreach (var child in toRemove)
                {
                    menu.Children.Remove(child);
                }
            }
        }

        // Obtener solo menús raíz válidos
        var rootMenus = menus
            .Where(m => m.ParentId == null)
            .ToList();

        return rootMenus.Select(menu => MapToDto(menu)).ToList();
    }

    private MenuItemResponse MapToDto(MenuItemOption menu)
    {
        return new MenuItemResponse
        (
            menu.Label,
            menu.Icon,
            menu.Route,
            menu.Children
                .Where(c => c.DeletedAt == null)
                .Select(MapToDto)
                .ToList(),
            menu.CreatedAt,
            menu.UpdatedAt,
            menu.CreatedBy,
            menu.UpdatedBy
        );
    }
}
