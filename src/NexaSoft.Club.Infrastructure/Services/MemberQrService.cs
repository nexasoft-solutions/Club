using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Services;

public class MemberQrService(
    IGenericRepository<Member> _memberRepository,
    IQrGeneratorService _qrGeneratorService,
    IDateTimeProvider _dateTimeProvider,
    ILogger<MemberQrService> _logger

): IMemberQrService
{
  
    public async Task<QrData> GenerateOrGetMonthlyQr(long memberId, CancellationToken cancellationToken)
    {
        try
        {
            var member = await _memberRepository.GetByIdAsync(memberId,cancellationToken);
            if (member == null)
                throw new Exception($"Member {memberId} no encontrado");

            // Verificar si el QR actual es válido (no ha expirado)
            if (member.QrExpiration.HasValue && 
                member.QrExpiration.Value.ToDateTime(TimeOnly.MinValue) > _dateTimeProvider.CurrentTime &&
                !string.IsNullOrEmpty(member.QrCode))
            {
                _logger.LogInformation("Usando QR existente para member {MemberId}", memberId);
                
                // Generar URL presigned si es necesario
                string? imageUrl = member.QrUrl;
                if (!string.IsNullOrEmpty(member.QrUrl))
                {
                    // Si necesitas regenerar la URL presigned
                    // imageUrl = await _qrGeneratorService.GetQrPresignedUrlAsync(extraerFileName(member.QrImageUrl));
                }

                return new QrData(
                    member.QrCode!, 
                    imageUrl, 
                    member.QrExpiration.Value.ToDateTime(TimeOnly.MinValue), 
                    IsNew: false
                );
            }

            // Si el QR expiró o no existe, generar uno nuevo
            _logger.LogInformation("Generando nuevo QR para member {MemberId}", memberId);
            
            var memberData = $"{member.Dni}|{member.FirstName} {member.LastName}|{_dateTimeProvider.CurrentTime:yyyyMM}";
            var qrResult = await _qrGeneratorService.GenerateMemberQrAsync(memberId, memberData, CancellationToken.None);

            // Calcular nueva expiración (fin de mes actual)
            var expiration = new DateTime(_dateTimeProvider.CurrentTime.Year, 
                                        _dateTimeProvider.CurrentTime.Month, 1)
                            .AddMonths(1).AddDays(-1);

            // Actualizar el member con el nuevo QR
            member.UpdateQr(
                qrResult.QrCode, 
                qrResult.FileUrl, 
                DateOnly.FromDateTime(expiration),
                "System-Auth" // O el usuario del sistema
            );

            await _memberRepository.UpdateAsync(member);
            
            _logger.LogInformation("Nuevo QR generado para member {MemberId}", memberId);

            return new QrData(
                qrResult.QrCode, 
                qrResult.FileUrl, 
                expiration, 
                IsNew: true
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generando QR para member {MemberId}", memberId);
            throw;
        }
    }

    public async Task<string> GetCurrentQrCode(long memberId, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(memberId,cancellationToken);
        return member?.QrCode ?? throw new Exception($"Member {memberId} no tiene QR generado");
    }

    public async Task<bool> ValidateQr(string qrCode, long memberId, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(memberId, cancellationToken);
        if (member == null) return false;

        // Validar que el QR coincida y no haya expirado
        return member.QrCode == qrCode && 
               member.QrExpiration.HasValue && 
               member.QrExpiration.Value.ToDateTime(TimeOnly.MinValue) >= _dateTimeProvider.CurrentTime;
    }
}