using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Menus;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenus;

public class GetMenusQueryHandler(IGenericRepository<MenuItemOption> _repository) : IQueryHandler<GetMenusQuery, Pagination<MenuResponse>>
{
    public async Task<Result<Pagination<MenuResponse>>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new MenuSpecification(request.SpecParams);
            var responses = await _repository.ListAsync(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<MenuResponse>(
                    request.SpecParams.PageIndex,
                    request.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<MenuResponse>>(MenuItemErrores.ErrorConsulta);
        }
    }
}
