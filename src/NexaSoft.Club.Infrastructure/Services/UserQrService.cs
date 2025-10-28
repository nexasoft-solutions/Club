using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Infrastructure.Services;

public class UserQrService(
    IGenericRepository<User> _userRepository,
    IQrGeneratorService _qrGeneratorService,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UserQrService> _logger

): IMemberQrService
{
  
    public async Task<QrData> GenerateOrGetMonthlyQr(long userId, CancellationToken cancellationToken)
    {
        try
        {
            
            var user = await _userRepository.GetByIdAsync(userId,cancellationToken);
            if (user == null)
                throw new Exception($"User {userId} no encontrado");

            // Verificar si el QR actual es válido (no ha expirado)
            if (user.QrExpiration.HasValue && 
                user.QrExpiration.Value.ToDateTime(TimeOnly.MinValue) > _dateTimeProvider.CurrentTime &&
                !string.IsNullOrEmpty(user.QrCode))
            {
                _logger.LogInformation("Usando QR existente para user {UserId}", userId);

                // Generar URL presigned si es necesario
                string? imageUrl = user.QrUrl;
                if (!string.IsNullOrEmpty(user.QrUrl))
                {
                    // Si necesitas regenerar la URL presigned
                     imageUrl = await _qrGeneratorService.GetQrPresignedUrlAsync(user.QrCode);
                }

                return new QrData(
                    user.QrCode!, 
                    imageUrl, 
                    user.QrExpiration.Value.ToDateTime(TimeOnly.MinValue), 
                    IsNew: false
                );
            }

            // Si el QR expiró o no existe, generar uno nuevo
            _logger.LogInformation("Generando nuevo QR para user {UserId}", userId);

            var userData = $"{user.Dni}|{user.FirstName} {user.LastName}|{_dateTimeProvider.CurrentTime:yyyyMM}";
            var qrResult = await _qrGeneratorService.GenerateMemberQrAsync(userId, userData, CancellationToken.None);

            // Calcular nueva expiración (fin de mes actual)
            var expiration = new DateTime(_dateTimeProvider.CurrentTime.Year, 
                                        _dateTimeProvider.CurrentTime.Month, 1)
                            .AddMonths(1).AddDays(-1);

            // Actualizar el user con el nuevo QR
            user.UpdateQr(
                qrResult.QrCode, 
                qrResult.FileUrl, 
                DateOnly.FromDateTime(expiration),
                "System-Auth" // O el usuario del sistema
            );

            await _userRepository.UpdateAsync(user);

            _logger.LogInformation("Nuevo QR generado para user {UserId}", userId);

            return new QrData(
                qrResult.QrCode, 
                qrResult.FileUrl, 
                expiration, 
                IsNew: true
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generando QR para user {UserId}", userId);
            throw;
        }
    }

    public async Task<string> GetCurrentQrCode(long userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId,cancellationToken);
        return user?.QrCode ?? throw new Exception($"User {userId} no tiene QR generado");
    }

    public async Task<bool> ValidateQr(string qrCode, long userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null) return false;

        // Validar que el QR coincida y no haya expirado
        return user.QrCode == qrCode && 
               user.QrExpiration.HasValue && 
               user.QrExpiration.Value.ToDateTime(TimeOnly.MinValue) >= _dateTimeProvider.CurrentTime;
    }
}