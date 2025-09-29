namespace NexaSoft.Club.Application.Storages;

public interface IFileStorageService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken);

    Task<string> GetPresignedUrlAsync(string fileName, int expirationInMinutes = 60); 
}
