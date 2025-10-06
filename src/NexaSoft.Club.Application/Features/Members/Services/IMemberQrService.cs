using System;

namespace NexaSoft.Club.Application.Features.Members.Services;

public interface IMemberQrService
{
    Task<QrData> GenerateOrGetMonthlyQr(long memberId, CancellationToken cancellationToken);
    Task<bool> ValidateQr(string qrCode, long memberId, CancellationToken cancellationToken);
    Task<string> GetCurrentQrCode(long memberId, CancellationToken cancellationToken); // Obtener QR actual del Member
}


public sealed record QrData(
    string QrCode,
    string? QrImageUrl,
    DateTime ExpirationDate,
    bool IsNew = false
);