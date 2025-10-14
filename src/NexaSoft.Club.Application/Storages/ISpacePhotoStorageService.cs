
namespace NexaSoft.Club.Application.Storages;

public interface ISpacePhotoStorageService
{
    Task<string> UploadSpacePhotoAsync(
             Stream photoStream,
             string originalFileName,
             string contentType,
             long spaceId,
             CancellationToken cancellationToken);

    Task<bool> DeleteSpacePhotoAsync(string fileName, CancellationToken cancellationToken);
    Task<string> GetSpacePhotoPresignedUrlAsync(string fileName, int expirationInMinutes = 60);
}
