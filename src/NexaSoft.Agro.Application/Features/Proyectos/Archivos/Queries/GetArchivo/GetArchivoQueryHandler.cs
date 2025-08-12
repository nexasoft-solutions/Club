using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivo;

public class GetArchivoQueryHandler(
    IArchivoRepository _repository
) : IQueryHandler<GetArchivoQuery, ArchivoResponse>
{
    public async Task<Result<ArchivoResponse>> Handle(GetArchivoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ArchivoSpecification(specParams);

            var (pagination, _) = await _repository.GetArchivosAsync(spec, cancellationToken);

            var entity = pagination.Data.FirstOrDefault();

            if (entity is null)
               return Result.Failure<ArchivoResponse>(ArchivoErrores.NoEncontrado);

           return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ArchivoResponse>(ArchivoErrores.ErrorConsulta);
        }
    }
}
