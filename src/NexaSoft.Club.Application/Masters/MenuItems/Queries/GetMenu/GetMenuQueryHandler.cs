using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Menus;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenu;

public class GetMenuQueryHandler(
    IGenericRepository<MenuItemOption> _repository
) : IQueryHandler<GetMenuQuery, MenuResponse>
{
    public async Task<Result<MenuResponse>> Handle(GetMenuQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new MenuSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<MenuResponse>(MenuItemErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<MenuResponse>(MenuItemErrores.ErrorConsulta);
        }
    }
}
