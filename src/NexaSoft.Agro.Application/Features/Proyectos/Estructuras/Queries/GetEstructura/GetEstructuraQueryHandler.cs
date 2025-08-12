using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Queries.GetEstructura;

public class GetEstructuraQueryHandler(
    IEstructuraRepository _repository
) : IQueryHandler<GetEstructuraQuery, EstructuraResponse>
{
    public async Task<Result<EstructuraResponse>> Handle(GetEstructuraQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EstructuraSpecification(specParams);

            var (pagination, _) = await _repository.GetEstructurasAsync(spec, cancellationToken);

            var entity = pagination.Data.FirstOrDefault();

            if (entity is null)
               return Result.Failure<EstructuraResponse>(EstructuraErrores.NoEncontrado);

           return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EstructuraResponse>(EstructuraErrores.ErrorConsulta);
        }
    }
}
