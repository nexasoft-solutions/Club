using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlanoByArchivo;

public class GetPlanoByArchivoQueryHandler(IPlanoRepository _repository) : IQueryHandler<GetPlanoByArchivoQuery, PlanoItemResponse>
{
    public async Task<Result<PlanoItemResponse>> Handle(GetPlanoByArchivoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetPlanoArchivoById(request.ArchivoId, cancellationToken);

            if (entity is null)
                return Result.Failure<PlanoItemResponse>(PlanoErrores.NoEncontrado);


            return entity;
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PlanoItemResponse>(PlanoErrores.ErrorConsulta);
        }
    }
}
