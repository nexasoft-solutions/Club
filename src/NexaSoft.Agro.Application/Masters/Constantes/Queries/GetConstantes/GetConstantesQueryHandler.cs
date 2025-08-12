using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstantes;

public class GetConstantesQueryHandler(
    IGenericRepository<Constante> _repository
) : IQueryHandler<GetConstantesQuery, Pagination<ConstanteResponse>>
{
    public async Task<Result<Pagination<ConstanteResponse>>> Handle(GetConstantesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            //var spec = new ConstanteSpecification(query.SpecParams);

            var spec = new ConstanteSpecification<ConstanteResponse>(
                query.SpecParams,
                x => new ConstanteResponse(
                    x.Id,
                    x.TipoConstante,
                    x.Clave,
                    x.Valor,
                    x.EstadoConstante,
                    x.FechaCreacion
                )
            );
            var responses = await _repository.ListAsync<ConstanteResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ConstanteResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            

            /*var responses = await _repository.ListAsync(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            // Agrupamos después de la paginación de items planos
            var agrupadas = responses
                .GroupBy(c => c.TipoConstante)
                .Select(g => new ConstanteAgrupadaResponse(
                    TipoConstante: g.Key!,
                    Data: g.ToList(),
                    Total: g.Count()
                ))
                .ToList();

            // Aquí podrías paginar los grupos si lo deseas (ejemplo opcional)
            var pagedAgrupadas = agrupadas
                .Skip((query.SpecParams.PageIndex - 1) * query.SpecParams.PageSize)
                .Take(query.SpecParams.PageSize)
                .ToList();

            var pagination = new Pagination<ConstanteResponse>(
                query.SpecParams.PageIndex,
                query.SpecParams.PageSize,
                agrupadas.Count, // total de grupos
                pagedAgrupadas
            );*/

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<ConstanteResponse>>(ConstanteErrores.ErrorConsulta);
        }
    }
}
