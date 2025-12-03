using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Menus;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetTreeMenus;

public class GetTreeMenusQueryHandler(
    IGenericRepository<MenuItemOption> _repository
) : IQueryHandler<GetTreeMenusQuery, List<MenuResponse>>
{
    public async Task<Result<List<MenuResponse>>> Handle(GetTreeMenusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams<long> { NoPaging = true };
            var spec = new MenuSpecification(specParams);
            var list = await _repository.ListAsync(spec, cancellationToken);
            if (list == null || !list.Any())
                return Result.Failure<List<MenuResponse>>(MenuItemErrores.NoEncontrado);
            // 🔹 Construir árbol jerárquico
            var tree = BuildMenuTree(list);

            return Result.Success(tree);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<MenuResponse>>(MenuItemErrores.ErrorConsulta);
        }
    }

    private static List<MenuResponse> BuildMenuTree(IEnumerable<MenuResponse> flatList)
    {
        var lookup = flatList.ToLookup(x => x.ParentId);

        List<MenuResponse> Build(long? parentId)
        {
            return lookup[parentId]
                .Select(x => x with
                {
                    Children = Build(x.Id)
                })
                .OrderBy(x => x.Label)
                .ToList();
        }

        return Build(null); // Raíces (sin padre)
    }
}
