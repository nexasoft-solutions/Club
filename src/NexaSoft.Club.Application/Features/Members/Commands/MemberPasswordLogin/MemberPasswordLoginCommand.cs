using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;

namespace NexaSoft.Club.Application.Features.Members.Commands.MemberPasswordLogin;

public sealed record MemberPasswordLoginCommand
(
    string Dni,
    string Password,
    string? DeviceId = null,
    string? BiometricToken = null
) : ICommand<MemberLoginResponse>;