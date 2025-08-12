using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Queries.GetEmpresa;

public class GetEmpresaQueryHandler(
    IGenericRepository<Empresa> _repository
) : IQueryHandler<GetEmpresaQuery, EmpresaResponse>
{
    public async Task<Result<EmpresaResponse>> Handle(GetEmpresaQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EmpresaSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<EmpresaResponse>(EmpresaErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EmpresaResponse>(EmpresaErrores.ErrorConsulta);
        }
    }
}
