using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Specifications;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Club.Application.Features.Members.Commands.MemberPasswordLogin;

public class MemberPasswordLoginCommandHandler(
    IGenericRepository<Member> _memberRepository,
    IMemberQrService _qrService,
    IMemberTokenService _memberTokenService,
    IUnitOfWork _unitOfWork,
    //IDateTimeProvider _dateTimeProvider,
    ILogger<MemberPasswordLoginCommandHandler> _logger
) : ICommandHandler<MemberPasswordLoginCommand, MemberLoginResponse>
{
    public async Task<Result<MemberLoginResponse>> Handle(
        MemberPasswordLoginCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login con password para DNI: {Dni}", command.Dni);

        try
        {
            // 1. Buscar member por DNI
            var member = await _memberRepository.GetEntityWithSpec(
                new MemberByDniSpec(command.Dni),
                cancellationToken);

            if (member == null)
                return Result.Failure<MemberLoginResponse>(MemberErrores.NoEncontrado);

            if (member.Status != "Active")
                return Result.Failure<MemberLoginResponse>(MemberErrores.NotActive);

            if (!member.CanUsePasswordAuth())
                return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorConfigPassword);

            // 2. Validar password O huella
            bool isValidAuth = false;

            if (!string.IsNullOrEmpty(command.BiometricToken) && member.BiometricToken == command.BiometricToken)
            {
                isValidAuth = true; // Login por huella
                _logger.LogInformation("Login por huella para member {MemberId}", member.Id);
            }
            else if (BC.Verify(command.Password, member.PasswordHash))//(member.VerifyPassword(command.Password))
            {

                isValidAuth = true; // Login por password
                _logger.LogInformation("Login por password para member {MemberId}", member.Id);
            }

            if (!isValidAuth)
                return Result.Failure<MemberLoginResponse>(MemberErrores.PasswordInvalido);

            // 3. Generar QR
            var qrData = await _qrService.GenerateOrGetMonthlyQr(member.Id, cancellationToken);

            // 4. Generar token
            var tokenResult = await _memberTokenService.GenerateMemberTokenWithPassword(
                member, command.Password, qrData, cancellationToken);

            if (!tokenResult.IsSuccess)
                return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorGenerarToken);

            // 5. Registrar login
            if (!string.IsNullOrEmpty(command.DeviceId))
            {
                member.RecordLogin(command.DeviceId);
                await _memberRepository.UpdateAsync(member);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }

            _logger.LogInformation("Login exitoso para member {MemberId}", member.Id);

            return Result.Success(new MemberLoginResponse(
                Token: tokenResult.Value.Token,
                RefreshToken: tokenResult.Value.RefreshToken,
                ExpiresAt: tokenResult.Value.ExpiresAt,
                MemberId: member.Id,
                MemberName: $"{member.FirstName} {member.LastName}",
                Email: member.Email!,
                Phone: member.Phone!,
                QrCode: qrData.QrCode,
                //QrImageUrl: qrData.QrImageUrl,
                QrExpiration: DateOnly.FromDateTime(qrData.ExpirationDate),
                Balance: member.Balance,
                MemberType: member.MemberType?.TypeName ?? "Standard",
                MembershipStatus: member.Status
            //HasSetPassword: member.HasSetPassword
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en login con password para DNI: {Dni}", command.Dni);
            return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorLogin);
        }
    }
}