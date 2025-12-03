using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuRoles;

public class GetMenuRolesQueryHandler(
    IMenuItemRepository _repository
) : IQueryHandler<GetMenuRolesQuery, List<long>>
{
    public async Task<Result<List<long>>> Handle(GetMenuRolesQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var roles = await _repository.GetUserRolesAsync(query.MenuId, cancellationToken);

            return Result.Success(roles);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<long>>(MenuItemErrores.ErrorConsulta);
        }
    }
}
