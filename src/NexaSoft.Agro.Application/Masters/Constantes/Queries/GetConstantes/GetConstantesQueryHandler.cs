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
                    x.FechaCreacion,
                    x.FechaModificacion,
                    x.UsuarioCreacion,
                    x.UsuarioModificacion
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

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<ConstanteResponse>>(ConstanteErrores.ErrorConsulta);
        }
    }
}
