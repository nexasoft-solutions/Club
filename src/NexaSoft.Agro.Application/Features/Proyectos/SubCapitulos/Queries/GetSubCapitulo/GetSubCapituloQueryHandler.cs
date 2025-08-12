using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Queries.GetSubCapitulo;

public class GetSubCapituloQueryHandler(
    IGenericRepository<SubCapitulo> _repository
) : IQueryHandler<GetSubCapituloQuery, SubCapituloResponse>
{
    public async Task<Result<SubCapituloResponse>> Handle(GetSubCapituloQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new SubCapituloSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<SubCapituloResponse>(SubCapituloErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SubCapituloResponse>(SubCapituloErrores.ErrorConsulta);
        }
    }
}
