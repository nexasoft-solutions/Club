using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuByUserAndRole;

public class GetMenuByUserAndRoleQueryHandler(
    IMenuItemRepository _repository
) : IQueryHandler<GetMenuByUserAndRoleQuery, List<MenuItemsResponse>>
{
    public async Task<Result<List<MenuItemsResponse>>> Handle(GetMenuByUserAndRoleQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var roles = await _repository.GetMenuByUserAndRoleAsync(query.UserId, query.RoleId, cancellationToken);

            return Result.Success(roles);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<MenuItemsResponse>>(MenuItemErrores.ErrorConsulta);
        }
    }
}
