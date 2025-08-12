using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivos;

public class GetArchivosQueryHandler(
    IArchivoRepository _repository
) : IQueryHandler<GetArchivosQuery, Pagination<ArchivoResponse>>
{
    public async Task<Result<Pagination<ArchivoResponse>>> Handle(GetArchivosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ArchivoSpecification(query.SpecParams);
            var (pagination, _) = await _repository.GetArchivosAsync(spec, cancellationToken);

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<ArchivoResponse>>(ArchivoErrores.ErrorConsulta);
        }
    }
}
