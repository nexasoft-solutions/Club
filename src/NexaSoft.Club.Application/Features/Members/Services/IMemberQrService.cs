namespace NexaSoft.Club.Application.Features.Members.Services;

public interface IMemberQrService
{
    Task<QrData> GenerateOrGetMonthlyQr(long userId, CancellationToken cancellationToken);
    Task<bool> ValidateQr(string qrCode, long userId, CancellationToken cancellationToken);
    Task<string> GetCurrentQrCode(long userId, CancellationToken cancellationToken); // Obtener QR actual del 

}


public sealed record QrData(
    string QrCode,
    string? QrImageUrl,
    DateTime ExpirationDate,
    bool IsNew = false
);