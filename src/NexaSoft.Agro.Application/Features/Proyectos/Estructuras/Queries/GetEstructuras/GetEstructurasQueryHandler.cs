using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Queries.GetEstructuras;

public class GetEstructurasQueryHandler(
    IEstructuraRepository _repository
) : IQueryHandler<GetEstructurasQuery, Pagination<EstructuraResponse>>
{
    public async Task<Result<Pagination<EstructuraResponse>>> Handle(GetEstructurasQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EstructuraSpecification(query.SpecParams);
            var (pagination, _) = await _repository.GetEstructurasAsync(spec, cancellationToken);

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<EstructuraResponse>>(EstructuraErrores.ErrorConsulta);
        }
    }
}
