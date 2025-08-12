
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Storages;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivoDownload;

public class GetArchivoDownloadQueryHandler(
    IGenericRepository<Archivo> _repository,
    IFileStorageService _storageService
) : IQueryHandler<GetArchivoDownloadQuery, string>
{
    public async Task<Result<string>> Handle(GetArchivoDownloadQuery query, CancellationToken cancellationToken)
    {
        var archivo = await _repository.GetByIdAsync(query.Id, cancellationToken);

        if (archivo is null)
            return Result.Failure<string>(ArchivoErrores.NoEncontrado);

        if (string.IsNullOrWhiteSpace(archivo.RutaArchivo))
            return Result.Failure<string>(ArchivoErrores.ErrorRutaValida);

        var url = await _storageService.GetPresignedUrlAsync(archivo.RutaArchivo);
        return Result.Success(url);
    }
}
