using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.Members.Events;

namespace NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

public class MemberQrGenerationEventHandler(
    IGenericRepository<Member> _memberRepository,
    IGenericRepository<MemberQrHistory> _qrHistoryRepository,
    IQrGeneratorService _qrGeneratorService,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<MemberQrGenerationEventHandler> _logger
) : INotificationHandler<MemberQrGenerationRequiredDomainEvent>
{
    public async Task Handle(MemberQrGenerationRequiredDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generando QR para member {MemberId}", notification.MemberId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. Cargar member
            var member = await _memberRepository.GetByIdAsync(notification.MemberId, cancellationToken);
            if (member == null)
            {
                _logger.LogWarning("Member {MemberId} no encontrado para generación de QR", notification.MemberId);
                return;
            }

            // 2. Verificar si está al día para renovación (solo para renovaciones)
            if (member.QrExpiration.HasValue && !member.IsUpToDateForQrRenewal())
            {
                _logger.LogWarning("Member {MemberId} no está al día, no se genera QR", notification.MemberId);
                return;
            }

            // 3. Generar nuevo QR
            var memberData = $"{member.Dni}|{member.FirstName} {member.LastName}|{member.JoinDate:yyyyMMdd}";
            var qrResult = await _qrGeneratorService.GenerateMemberQrAsync(
                member.Id, memberData, cancellationToken);

            // 4. Calcular fecha de expiración
            var expirationDate = CalculateQrExpirationDate(member.JoinDate, member.QrExpiration);

            // 5. Actualizar member con nuevo QR
            member.UpdateQr(qrResult.QrCode, qrResult.FileUrl, expirationDate,notification.CreatedBy);

            // 6. Guardar en historial
            var qrHistory = MemberQrHistory.Create(
                member.Id,
                qrResult.QrCode,
                qrResult.FileUrl,
                expirationDate,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                notification.CreatedBy
            );

            await _qrHistoryRepository.AddAsync(qrHistory, cancellationToken);
            await _memberRepository.UpdateAsync(member);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("QR generado exitosamente para member {MemberId}. Expira: {ExpirationDate}",
                member.Id, expirationDate);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error generando QR para member {MemberId}", notification.MemberId);
        }
    }

    private DateOnly CalculateQrExpirationDate(DateOnly joinDate, DateOnly? currentExpiration)
    {
        if (!currentExpiration.HasValue)
        {
            // Primer QR: expira en 3 meses desde hoy
            return DateOnly.FromDateTime(DateTime.Now.AddMonths(3));
        }
        else
        {
            // Renovación: expira 3 meses después de la 3ra cuota
            var thirdFeeDate = joinDate.AddMonths(2);
            return thirdFeeDate.AddMonths(3);
        }
    }
}
