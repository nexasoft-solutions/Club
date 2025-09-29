using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Constantes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstantes;

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
                    x.CreatedAt,
                    x.UpdatedAt,
                    x.CreatedBy,
                    x.UpdatedBy
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
