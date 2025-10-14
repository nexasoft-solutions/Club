using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Storages;

namespace NexaSoft.Club.Infrastructure.Services;

public class SpacePhotoStorageService(
        IFileStorageService _fileStorageService,
        ILogger<SpacePhotoStorageService> _logger
) : ISpacePhotoStorageService
{
    public async Task<string> UploadSpacePhotoAsync(
        Stream photoStream,
        string originalFileName,
        string contentType,
        long spaceId,
        CancellationToken cancellationToken)
    {
        try
        {
            // Validar el archivo
            ValidatePhotoFile(photoStream, originalFileName, contentType);

            // Generar nombre único del archivo usando tu método
            var extension = Path.GetExtension(originalFileName).ToLowerInvariant();
            var fileName = GenerateFileName(spaceId, extension);

            // Subir a MinIO (usamos el stream directamente)
            var fileUrl = await _fileStorageService.UploadAsync(
                photoStream,
                fileName,
                contentType,
                cancellationToken);

            _logger.LogInformation("Foto de espacio subida exitosamente. SpaceId: {SpaceId}, File: {FileName}",
                spaceId, fileName);

            return fileUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error subiendo foto de espacio. SpaceId: {SpaceId}", spaceId);
            throw;
        }
    }

    public async Task<bool> DeleteSpacePhotoAsync(string fileName, CancellationToken cancellationToken)
    {
        try
        {
            await _fileStorageService.DeleteAsync(fileName, cancellationToken);
            _logger.LogInformation("Foto de espacio eliminada: {FileName}", fileName);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error eliminando foto de espacio: {FileName}", fileName);
            return false;
        }
    }

    public async Task<string> GetSpacePhotoPresignedUrlAsync(string fileName, int expirationInMinutes = 60)
    {
        return await _fileStorageService.GetPresignedUrlAsync(fileName, expirationInMinutes);
    }

    private void ValidatePhotoFile(Stream photoStream, string originalFileName, string contentType)
    {
        if (photoStream == null || photoStream.Length == 0)
            throw new ArgumentException("El stream de foto no puede estar vacío");

        if (!photoStream.CanRead)
            throw new ArgumentException("El stream no es legible");

        // Validar tipos de archivo permitidos
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };

        var extension = Path.GetExtension(originalFileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException($"Tipo de archivo no permitido: {extension}");

        if (!allowedMimeTypes.Contains(contentType.ToLowerInvariant()))
            throw new ArgumentException($"Tipo MIME no permitido: {contentType}");

        // Validar tamaño (máximo 10MB)
        if (photoStream.Length > 10 * 1024 * 1024)
            throw new ArgumentException("El archivo es demasiado grande. Tamaño máximo: 10MB");
    }

    private string GenerateFileName(long spaceId, string extension)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var randomId = Guid.NewGuid().ToString("N").Substring(0, 8);
        return $"space-photos/{spaceId}/{timestamp}_{randomId}{extension}";
    }
}