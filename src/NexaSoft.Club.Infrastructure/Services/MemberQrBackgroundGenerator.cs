using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Infrastructure.Services;

public class MemberQrBackgroundGenerator: IMemberQrBackgroundGenerator
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Member> _memberRepository;
    private readonly IGenericRepository<UserQrHistory> _qrHistoryRepository;
    private readonly IQrGeneratorService _qrGeneratorService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<MemberQrBackgroundGenerator> _logger;

    public MemberQrBackgroundGenerator(
        IGenericRepository<User> userRepository,
        IGenericRepository<Member> memberRepository,
        IGenericRepository<UserQrHistory> qrHistoryRepository,
        IQrGeneratorService qrGeneratorService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        ILogger<MemberQrBackgroundGenerator> logger)
    {
        _userRepository = userRepository;
        _memberRepository = memberRepository;
        _qrHistoryRepository = qrHistoryRepository;
        _qrGeneratorService = qrGeneratorService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task GenerateUserQrAsync(long userId, long memberId, string createdBy, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generando QR en background para user {UserId}", userId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. Cargar user
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new InvalidOperationException($"User {userId} no encontrado");
            }

            var member = await _memberRepository.GetByIdAsync(memberId, cancellationToken);
            if (member == null)
            {
                throw new InvalidOperationException($"Member {memberId} no encontrado");
            }

            // 2. Verificar si está al día (solo para renovaciones)
            if (user.QrExpiration.HasValue && !member.IsUpToDateForQrRenewal())
            {
                _logger.LogWarning("User {UserId} no está al día, no se genera QR", userId);
                return;
            }

            // 3. Generar QR
            var userData = $"{user.Dni}|{user.FirstName} {user.LastName}|{member.JoinDate:yyyyMMdd}";
            var qrResult = await _qrGeneratorService.GenerateMemberQrAsync(member.Id, userData, cancellationToken);

            // 4. Calcular fecha de expiración
            var expirationDate = CalculateQrExpirationDate(member.JoinDate, user.QrExpiration);

            // 5. Actualizar member con nuevo QR
            user.UpdateQr(qrResult.QrCode, qrResult.FileUrl, expirationDate, member.CreatedBy!);

            // 6. Guardar en historial
            var qrHistory = UserQrHistory.Create(
                user.Id,
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

            _logger.LogInformation("QR generado exitosamente para user {UserId}", userId);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error generando QR para user {UserId}", userId);
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