using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstante;

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
                   x.FechaCreacion,
                   x.FechaModificacion,
                   x.UsuarioCreacion,
                   x.UsuarioModificacion
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
