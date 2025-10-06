using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Application.Storages;
using QRCoder;

namespace NexaSoft.Club.Infrastructure.Services;

public class QrGeneratorService(
    IFileStorageService _fileStorageService,
    ILogger<QrGeneratorService> _logger
) : IQrGeneratorService
{

    public async Task<QrGenerationResult> GenerateMemberQrAsync(long memberId, string memberData, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Generar código único del QR
            var qrCode = $"CLUB-{memberId}-{DateTime.Now:yyyyMMddHHmmss}";

            // 2. Generar imagen del QR
            var qrImageBytes = GenerateQrImage(qrCode, memberData);

            // 3. Subir a MinIO
            var fileName = $"member-qrs/{memberId}/qrcodes/{qrCode}.png";

            using var stream = new MemoryStream(qrImageBytes);
            var fileUrl = await _fileStorageService.UploadAsync(
                stream,
                fileName,
                "image/png",
                cancellationToken);

            _logger.LogInformation("QR generado para member {MemberId}: {FileName}", memberId, fileName);

            return new QrGenerationResult(qrCode, qrImageBytes, fileName, fileUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generando QR para member {MemberId}", memberId);
            throw;
        }
    }

    private byte[] GenerateQrImage(string qrCode, string memberData)
    {
        var qrContent = $"{qrCode}|{memberData}";

        using var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
        using var qrCodeImage = new PngByteQRCode(qrCodeData);

        return qrCodeImage.GetGraphic(20);
    }

    public async Task<string> GetQrPresignedUrlAsync(string fileName, int expirationInMinutes = 60)
    {
        return await _fileStorageService.GetPresignedUrlAsync(fileName, expirationInMinutes);
    }
}