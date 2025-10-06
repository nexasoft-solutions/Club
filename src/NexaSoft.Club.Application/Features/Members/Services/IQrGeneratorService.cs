
namespace NexaSoft.Club.Application.Features.Members.Services;

public interface IQrGeneratorService
{
    Task<QrGenerationResult> GenerateMemberQrAsync(long memberId, string memberData, CancellationToken cancellationToken);
    Task<string> GetQrPresignedUrlAsync(string fileName, int expirationInMinutes = 60);
        
}


public sealed record QrGenerationResult(
    string QrCode,    // Código único del QR
    byte[] QrImage,   // Imagen del QR en bytes
    string FileName,  // Nombre del archivo en MinIO
    string FileUrl    // URL/path del archivo
);

