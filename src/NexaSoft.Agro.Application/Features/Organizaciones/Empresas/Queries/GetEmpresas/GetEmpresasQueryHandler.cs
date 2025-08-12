using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresas;

public class GetEmpresasQueryHandler(
    IGenericRepository<Empresa> _repository
) : IQueryHandler<GetEmpresasQuery, Pagination<EmpresaResponse>>
{
    public async Task<Result<Pagination<EmpresaResponse>>> Handle(GetEmpresasQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EmpresaSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<EmpresaResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<EmpresaResponse>(
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
            return Result.Failure<Pagination<EmpresaResponse>>(EmpresaErrores.ErrorConsulta);
        }
    }
}
