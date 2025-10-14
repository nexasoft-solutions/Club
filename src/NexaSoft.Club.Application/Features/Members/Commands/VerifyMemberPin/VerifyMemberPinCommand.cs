using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;

public sealed record VerifyMemberPinCommand(
    string Dni,
    DateOnly BirthDate,
    string Pin, 
    string DeviceId
) : ICommand<MemberLoginResponse>;

public sealed record MemberLoginResponse(
    string Token,
    string RefreshToken,
    DateTime ExpiresAt,
    long MemberId,
    string MemberName,
    string Email,
    string Phone,
    string QrCode,
    DateOnly? QrExpiration,
    decimal Balance,
    string MemberType,
    string UserName,
    long? MembershipStatus
);
