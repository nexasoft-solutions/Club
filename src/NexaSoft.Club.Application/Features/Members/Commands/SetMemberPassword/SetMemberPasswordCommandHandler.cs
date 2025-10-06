
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Club.Application.Features.Members.Commands.SetMemberPassword;

public class SetMemberPasswordCommandHandler(
    IGenericRepository<Member> _memberRepository,
    IUnitOfWork _unitOfWork,
    ILogger<SetMemberPasswordCommandHandler> _logger
) : ICommandHandler<SetMemberPasswordCommand, bool>
{
    public async Task<Result<bool>> Handle(
        SetMemberPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(command.MemberId, cancellationToken);

        if (member == null)
            return Result.Failure<bool>(MemberErrores.NoEncontrado);

        // Validar password (mínimo 6 caracteres)
        if (command.Password.Length < 6)
            return Result.Failure<bool>(MemberErrores.PasswordErrado);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {


            member.SetPassword(BC.HashPassword(command.Password));

            if (!string.IsNullOrEmpty(command.BiometricToken))
                member.SetBiometricToken(command.BiometricToken);

            member.RecordLogin(command.DeviceId);

            await _memberRepository.UpdateAsync(member);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Password configurado para member {MemberId}", command.MemberId);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Member");
            return Result.Failure<bool>(MemberErrores.ErrorSave);
        }
    }
}