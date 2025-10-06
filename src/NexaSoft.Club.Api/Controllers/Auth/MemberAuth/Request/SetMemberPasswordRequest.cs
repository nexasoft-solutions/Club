using System;

namespace NexaSoft.Club.Api.Controllers.Auth.MemberAuth.Request;

public sealed record SetMemberPasswordRequest
(
    long MemberId,
    string Password,
    string? BiometricToken, // Para guardar huella/face ID
    string DeviceId
);
