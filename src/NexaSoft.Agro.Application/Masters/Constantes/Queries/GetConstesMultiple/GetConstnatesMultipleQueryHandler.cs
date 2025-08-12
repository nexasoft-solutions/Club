
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Specifications;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;

public class GetConstnatesMultipleQueryHandler(IGenericRepository<Constante> _repository) : IQueryHandler<GetConstnatesMultipleQuery, IReadOnlyList<ConstamtesAgrupadasResponse>>
{
    public async Task<Result<IReadOnlyList<ConstamtesAgrupadasResponse>>> Handle(GetConstnatesMultipleQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ConstanteMultipleSpecification(query.TiposConstante);
            var list = await _repository.ListAsync(spec, cancellationToken);

            var agrupadas = list
                .GroupBy(c => c.TipoConstante)
                .Select(g => new ConstamtesAgrupadasResponse(
                    g.Key!,
                    g.Select(c => new ConstanteClaveValorResponse(
                        c.Clave,
                        c.Valor!
                    )).ToList()
                ))
                .ToList();

            return Result.Success<IReadOnlyList<ConstamtesAgrupadasResponse>>(agrupadas);
        }
        catch (Exception ex)
        {

            var errores = ex.Message;
            return Result.Failure<IReadOnlyList<ConstamtesAgrupadasResponse>>(ConstanteErrores.ErrorConsulta);
        }

    }
}
