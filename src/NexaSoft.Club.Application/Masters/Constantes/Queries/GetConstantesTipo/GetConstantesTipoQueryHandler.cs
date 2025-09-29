using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Constantes;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstantesTipo;

public class GetConstantesTipoQueryHandler(IGenericRepository<Constante> _repository) : IQueryHandler<GetConstantesTipoQuery, List<string?>>
{
    public async Task<Result<List<string?>>> Handle(GetConstantesTipoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allConstantes = await _repository.ListAsync(cancellationToken);
            var tiposUnicos = allConstantes
                .Select(c => c.TipoConstante)
                .Distinct()
                .OrderBy(tipo => tipo)
                .ToList();

            return Result.Success(tiposUnicos);
        }
        catch (Exception ex)
        {

             var errores = ex.Message;
            return Result.Failure<List<string?>>(ConstanteErrores.ErrorConsulta);
        }

    }
}
