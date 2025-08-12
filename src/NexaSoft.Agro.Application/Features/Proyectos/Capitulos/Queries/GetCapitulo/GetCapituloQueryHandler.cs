using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Queries.GetCapitulo;

public class GetCapituloQueryHandler(
    IGenericRepository<Capitulo> _repository
) : IQueryHandler<GetCapituloQuery, CapituloResponse>
{
    public async Task<Result<CapituloResponse>> Handle(GetCapituloQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new CapituloSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<CapituloResponse>(CapituloErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<CapituloResponse>(CapituloErrores.ErrorConsulta);
        }
    }
}
