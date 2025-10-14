using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Application.Features.Members.Commands.RefreshMemberToken;

public class RefreshMemberTokenCommandHandler(
    IMemberTokenService _memberTokenService,
    IMemberQrService _qrService,
    ILogger<RefreshMemberTokenCommandHandler> _logger
) : ICommandHandler<RefreshMemberTokenCommand, MemberLoginResponse>
{
    public async Task<Result<MemberLoginResponse>> Handle(
        RefreshMemberTokenCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Refrescando token para device: {DeviceId}", command.DeviceId);

        try
        {
            // 1. Refrescar token usando el servicio
            var tokenResult = await _memberTokenService.RefreshMemberToken(command.RefreshToken, cancellationToken);

            if (!tokenResult.IsSuccess)
                return Result.Failure<MemberLoginResponse>(tokenResult.Error);

            // 2. Obtener member para datos adicionales
            var refreshTokenResult = await _memberTokenService.GetByRefreshTokenAsync(command.RefreshToken, cancellationToken);
            if (!refreshTokenResult.IsSuccess || refreshTokenResult.Value == null)
                return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorDatosAdicionales);

            var member = refreshTokenResult.Value.Member;
            var qrData = await _qrService.GenerateOrGetMonthlyQr(member.Id, cancellationToken);

            _logger.LogInformation("Token refrescado exitosamente para member {MemberId}", member.Id);

            return Result.Success(new MemberLoginResponse(
                Token: tokenResult.Value.Token,
                RefreshToken: tokenResult.Value.RefreshToken,
                ExpiresAt: tokenResult.Value.ExpiresAt,
                MemberId: member.Id,
                MemberName: $"{member.FirstName} {member.LastName}",
                Email: member.Email!,
                Phone: member.Phone!,
                QrCode: qrData.QrCode,
                QrExpiration: DateOnly.FromDateTime(qrData.ExpirationDate),
                Balance: member.Balance,
                MemberType: member.MemberType?.TypeName ?? "Regular",
                MembershipStatus: member.StatusId,
                UserName: "N/A"//refreshTokenResult.Value.User?.UserName ?? "N/A"
            //HasSetPassword: member.HasSetPassword
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refrescando token para device: {DeviceId}", command.DeviceId);
            return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorRefreshToken);
        }
    }
}