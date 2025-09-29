
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuByUser;

public class GetMenuByUserQueryHandler(IMenuItemRepository _repository) : IQueryHandler<GetMenuByUserQuery, List<MenuItemResponse>>
{
    public async Task<Result<List<MenuItemResponse>>> Handle(GetMenuByUserQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var menus = await _repository.GetMenuByUser(
                query.IdUser,
                query.IdRole,
                cancellationToken);
            return Result.Success(menus);
        }
        catch (Exception ex)
        {

            var errores = ex.Message;
            return Result.Failure<List<MenuItemResponse>>(MenuItemErrores.ErrorConsulta);
        }

    }
}
