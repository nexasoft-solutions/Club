using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Users;
using NexaSoft.Club.Domain.Specifications;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Club.Application.Features.Members.Commands.MemberPasswordLogin;

public class MemberPasswordLoginCommandHandler(
    IGenericRepository<User> _userRepository,
    IMemberQrService _qrService,
    IMemberTokenService _memberTokenService,
    IUnitOfWork _unitOfWork,
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
            var user = await _userRepository.GetEntityWithSpec(
                new UserByDniSpec(command.Dni),
                cancellationToken);

            if (user == null)
                return Result.Failure<MemberLoginResponse>(MemberErrores.NoEncontrado);

            if (!user.CanUsePasswordAuth())
                return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorConfigPassword);

            // 2. Validar password O huella
            bool isValidAuth = false;

            if (!string.IsNullOrEmpty(command.BiometricToken) && user.BiometricToken == command.BiometricToken)
            {
                isValidAuth = true; // Login por huella
                _logger.LogInformation("Login por huella para user {UserId}", user.Id);
            }
            else if (BC.Verify(command.Password, user.Password))
            {

                isValidAuth = true; // Login por password
                _logger.LogInformation("Login por password para user {UserId}", user.Id);
            }

            if (!isValidAuth)
                return Result.Failure<MemberLoginResponse>(MemberErrores.PasswordInvalido);

            // 3. Generar QR
            var qrData = await _qrService.GenerateOrGetMonthlyQr(user.Id, cancellationToken);

            // 4. Generar token
            var tokenResult = await _memberTokenService.GenerateMemberTokenWithPassword(
                user, command.Password, qrData, cancellationToken);

            if (!tokenResult.IsSuccess)
                return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorGenerarToken);

            // 5. Registrar login
            if (!string.IsNullOrEmpty(command.DeviceId))
            {
                user.RecordLogin(command.DeviceId);
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }

            _logger.LogInformation("Login exitoso para user {UserId}", user.Id);

            return Result.Success(new MemberLoginResponse(
                Token: tokenResult.Value.Token,
                RefreshToken: tokenResult.Value.RefreshToken,
                ExpiresAt: tokenResult.Value.ExpiresAt,
                MemberId: user.MemberId ?? 0,
                MemberName: $"{user.FullName}",
                Email: user.Email!,
                Phone: user.Phone!,
                QrCode: qrData.QrCode,
                QrExpiration: DateOnly.FromDateTime(qrData.ExpirationDate),
                Balance: user.Member!.Balance,
                MemberType: user.Member!.MemberType?.TypeName ?? "Standard",
                UserName: user.UserName!,   
                MembershipStatus: user.Member!.StatusId!
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en login con password para DNI: {Dni}", command.Dni);
            return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorLogin);
        }
    }
}