using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Services;

public class MemberQrBackgroundGenerator: IMemberQrBackgroundGenerator
{
    private readonly IGenericRepository<Member> _memberRepository;
    private readonly IGenericRepository<MemberQrHistory> _qrHistoryRepository;
    private readonly IQrGeneratorService _qrGeneratorService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<MemberQrBackgroundGenerator> _logger;

    public MemberQrBackgroundGenerator(
        IGenericRepository<Member> memberRepository,
        IGenericRepository<MemberQrHistory> qrHistoryRepository,
        IQrGeneratorService qrGeneratorService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        ILogger<MemberQrBackgroundGenerator> logger)
    {
        _memberRepository = memberRepository;
        _qrHistoryRepository = qrHistoryRepository;
        _qrGeneratorService = qrGeneratorService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task GenerateMemberQrAsync(long memberId, string createdBy, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generando QR en background para member {MemberId}", memberId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. Cargar member
            var member = await _memberRepository.GetByIdAsync(memberId, cancellationToken);
            if (member == null)
            {
                throw new InvalidOperationException($"Member {memberId} no encontrado");
            }

            // 2. Verificar si está al día (solo para renovaciones)
            if (member.QrExpiration.HasValue && !member.IsUpToDateForQrRenewal())
            {
                _logger.LogWarning("Member {MemberId} no está al día, no se genera QR", memberId);
                return;
            }

            // 3. Generar QR
            var memberData = $"{member.Dni}|{member.FirstName} {member.LastName}|{member.JoinDate:yyyyMMdd}";
            var qrResult = await _qrGeneratorService.GenerateMemberQrAsync(member.Id, memberData, cancellationToken);

            // 4. Calcular fecha de expiración
            var expirationDate = CalculateQrExpirationDate(member.JoinDate, member.QrExpiration);

            // 5. Actualizar member con nuevo QR
            member.UpdateQr(qrResult.QrCode, qrResult.FileUrl, expirationDate,member.CreatedBy!);

            // 6. Guardar en historial
            var qrHistory = MemberQrHistory.Create(
                member.Id,
                qrResult.QrCode,
                qrResult.FileUrl,
                expirationDate,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                createdBy
            );

            await _qrHistoryRepository.AddAsync(qrHistory, cancellationToken);
            await _memberRepository.UpdateAsync(member);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("QR generado exitosamente para member {MemberId}", memberId);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error generando QR para member {MemberId}", memberId);
            throw;
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