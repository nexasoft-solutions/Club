using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;

namespace NexaSoft.Club.Application.Features.Members.Commands.RefreshMemberToken;

public sealed record RefreshMemberTokenCommand(
    string RefreshToken,
    string DeviceId
) : ICommand<MemberLoginResponse>;
