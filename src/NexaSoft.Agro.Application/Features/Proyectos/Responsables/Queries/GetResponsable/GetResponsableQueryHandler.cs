using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Queries.GetResponsable;

public class GetResponsableQueryHandler(
    IGenericRepository<Responsable> _repository
) : IQueryHandler<GetResponsableQuery, ResponsableResponse>
{
    public async Task<Result<ResponsableResponse>> Handle(GetResponsableQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams<long> { Id = query.Id };
            var spec = new ResponsableSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
            if (entity is null)
                return Result.Failure<ResponsableResponse>(ResponsableErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ResponsableResponse>(ResponsableErrores.ErrorConsulta);
        }
    }
}
