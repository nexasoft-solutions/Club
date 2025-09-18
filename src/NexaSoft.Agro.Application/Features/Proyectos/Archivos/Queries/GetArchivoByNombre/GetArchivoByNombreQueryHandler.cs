using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivoByNombre;

public class GetArchivoByNombreQueryHandler(IArchivoRepository _archivoRepository) : IQueryHandler<GetArchivoByNombreQuery, List<ArchivoItemResponse>>
{
    public async Task<Result<List<ArchivoItemResponse>>> Handle(GetArchivoByNombreQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var list = await _archivoRepository.GetArchivosByNombreCortoAsync(
                query.EstudioAmbientalId,
                query.Descripcion, cancellationToken);

            return Result.Success(list);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<ArchivoItemResponse>>(ArchivoErrores.ErrorConsulta);
        }
    }
}
