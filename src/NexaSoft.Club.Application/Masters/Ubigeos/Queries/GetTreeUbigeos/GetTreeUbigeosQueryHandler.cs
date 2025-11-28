
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Ubigeos;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Queries.GetTreeUbigeos;

public class GetTreeUbigeosQueryHandler(
    IGenericRepository<Ubigeo> _repository
) : IQueryHandler<GetTreeUbigeosQuery, List<UbigeoResponse>>
{
    public async Task<Result<List<UbigeoResponse>>> Handle(GetTreeUbigeosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams<long> { NoPaging = true };
            var spec = new UbigeoSpecification(specParams);
            var list = await _repository.ListAsync(spec, cancellationToken);
            if (list == null || !list.Any())
                return Result.Failure<List<UbigeoResponse>>(UbigeoErrores.NoEncontrado);
            // 🔹 Construir árbol jerárquico
            var tree = BuildUbigeoTree(list);

            return Result.Success(tree);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<UbigeoResponse>>(UbigeoErrores.ErrorConsulta);
        }
    }

    private static List<UbigeoResponse> BuildUbigeoTree(IEnumerable<UbigeoResponse> flatList)
    {
        var lookup = flatList.ToLookup(x => x.ParentId);

        List<UbigeoResponse> Build(long? parentId)
        {
            return lookup[parentId]
                .Select(x => x with
                {
                    Children = Build(x.Id)
                })
                .OrderBy(x => x.Description)
                .ToList();
        }

        return Build(null); // Raíces (sin padre)
    }
}
