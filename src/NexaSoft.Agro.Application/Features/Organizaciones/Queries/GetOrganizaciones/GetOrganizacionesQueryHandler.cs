using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Organizaciones;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizaciones;

public class GetOrganizacionesQueryHandler(
    IOrganizacionRepository _repository
) : IQueryHandler<GetOrganizacionesQuery, Pagination<OrganizacionResponse>>
{
    public async Task<Result<Pagination<OrganizacionResponse>>> Handle(GetOrganizacionesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new OrganizacionSpecification(query.SpecParams);
            var (pagination, _) = await _repository.GetOrganizacionesAsync(spec, cancellationToken);

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<OrganizacionResponse>>(OrganizacionErrores.ErrorConsulta);
        }
    }
}
