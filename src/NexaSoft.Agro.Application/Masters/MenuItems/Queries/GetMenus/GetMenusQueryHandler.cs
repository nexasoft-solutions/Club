using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Menus;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Queries.GetMenus;

public class GetMenusQueryHandler(IGenericRepository<MenuItemOption> _repository) : IQueryHandler<GetMenusQuery, List<MenuResponse>>
{
    public async Task<Result<List<MenuResponse>>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.ListAsync(cancellationToken);

            var menus = result.Select(r => new MenuResponse
            (
                r.Id,
                r.Label,
                r.Icon,
                r.Route,
                r.ParentId
            )).ToList();

            return Result.Success(menus);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<MenuResponse>>(MenuItemErrores.ErrorConsulta);
        }
    }
}
