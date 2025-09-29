
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Constantes;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstesMultiple;

public class GetConstnatesMultipleQueryHandler(IGenericRepository<Constante> _repository) : IQueryHandler<GetConstnatesMultipleQuery, IReadOnlyList<ConstantesAgrupadasResponse>>
{
    public async Task<Result<IReadOnlyList<ConstantesAgrupadasResponse>>> Handle(GetConstnatesMultipleQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ConstanteMultipleSpecification(query.TiposConstante);
            var list = await _repository.ListAsync(spec, cancellationToken);

            var agrupadas = list
                .GroupBy(c => c.TipoConstante)
                .Select(g => new ConstantesAgrupadasResponse(
                    g.Key!,
                    g.Select(c => new ConstantesClaveValorResponse(
                        c.Clave,
                        c.Valor!
                    )).ToList()
                ))
                .ToList();

            return Result.Success<IReadOnlyList<ConstantesAgrupadasResponse>>(agrupadas);
        }
        catch (Exception ex)
        {

            var errores = ex.Message;
            return Result.Failure<IReadOnlyList<ConstantesAgrupadasResponse>>(ConstanteErrores.ErrorConsulta);
        }

    }
}
