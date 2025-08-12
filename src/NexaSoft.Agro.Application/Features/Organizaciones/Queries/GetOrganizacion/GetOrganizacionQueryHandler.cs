using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Queries.GetOrganizacion;

public class GetOrganizacionQueryHandler(
    IOrganizacionRepository _repository
) : IQueryHandler<GetOrganizacionQuery, OrganizacionResponse>
{
    public async Task<Result<OrganizacionResponse>> Handle(GetOrganizacionQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams<int> { Id = query.Id };
            var spec = new OrganizacionSpecification(specParams);

            var (pagination, _) = await _repository.GetOrganizacionesAsync(spec, cancellationToken);

            var entity = pagination.Data.FirstOrDefault();

            if (entity is null)
               return Result.Failure<OrganizacionResponse>(OrganizacionErrores.NoEncontrado);

           return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<OrganizacionResponse>(OrganizacionErrores.ErrorConsulta);
        }
    }
}
