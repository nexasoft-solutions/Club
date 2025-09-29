using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Constantes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstante;

public class GetConstanteQueryHandler(
    IGenericRepository<Constante> _repository
) : IQueryHandler<GetConstanteQuery, ConstanteResponse>
{
    public async Task<Result<ConstanteResponse>> Handle(GetConstanteQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            //var spec = new ConstanteSpecification(specParams);
            var spec = new ConstanteSpecification<ConstanteResponse>(
               specParams,
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
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
            if (entity is null)
                return Result.Failure<ConstanteResponse>(ConstanteErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ConstanteResponse>(ConstanteErrores.ErrorConsulta);
        }
    }
}
