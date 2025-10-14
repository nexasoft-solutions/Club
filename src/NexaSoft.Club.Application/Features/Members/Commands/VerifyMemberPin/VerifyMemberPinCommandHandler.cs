using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Users;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;

public class VerifyMemberPinCommandHandler(
    IGenericRepository<Member> _memberRepository,
    IGenericRepository<User> _userRepository,
    IGenericRepository<MemberPin> _pinRepository,
    IMemberQrService _qrService, // Ahora usa tu sistema de QR existente
    IMemberTokenService _memberTokenService,
    IUnitOfWork _unitOfWork,
    ILogger<VerifyMemberPinCommandHandler> _logger
) : ICommandHandler<VerifyMemberPinCommand, MemberLoginResponse>
{
    public async Task<Result<MemberLoginResponse>> Handle(
        VerifyMemberPinCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Verificando PIN para DNI: {Dni} y {BirthDate}", command.Dni, command.BirthDate);

        try
        {
            // 1. Buscar member por DNI

            var member = await _memberRepository.GetEntityWithSpec(
                new MemberByDniAndBirthDateSpec(command.Dni, command.BirthDate),
                cancellationToken);

            if (member == null)
                return Result.Failure<MemberLoginResponse>(MemberErrores.NoEncontrado);

            var user = await _userRepository.GetEntityWithSpec(
                new UserByMemberIdSpec(member!.Id),
                cancellationToken);

            if (user == null)
                return Result.Failure<MemberLoginResponse>(MemberErrores.NoEncontrado);

            /*if (member.StatusId != (int)StatusEnum.Activo)
                return Result.Failure<MemberLoginResponse>(MemberErrores.NotActive);*/

            // 2. Validar PIN
            var validPin = await _pinRepository.GetEntityWithSpec(
                new ValidPinSpec(member.Id, command.Pin, command.DeviceId),
                cancellationToken);

            if (validPin == null)
                return Result.Failure<MemberLoginResponse>(MemberErrores.PinInvalido);

            // 3. Marcar PIN como usado
            validPin.MarkAsUsed();
            await _pinRepository.UpdateAsync(validPin);

            // 4. Obtener QR (usa tu sistema existente a través de IMemberQrService)
            var qrData = await _qrService.GenerateOrGetMonthlyQr(member.Id, cancellationToken);

            // 5. Generar token JWT
            var tokenResult = await _memberTokenService.GenerateMemberToken(member, qrData, cancellationToken);

            if (!tokenResult.IsSuccess)
                return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorGenerarToken);

            // 6. Registrar primer login
            user.RecordLogin(command.DeviceId);
            await _userRepository.UpdateAsync(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("PIN validado exitosamente para member {MemberId}", member.Id);

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
                UserName: user.UserName!,
                MemberType: member.MemberType?.TypeName ?? "Regular",
                MembershipStatus: member.StatusId
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verificando PIN para DNI: {Dni}", command.Dni);
            return Result.Failure<MemberLoginResponse>(MemberErrores.ErrorValidandoPin);
        }
    }
}