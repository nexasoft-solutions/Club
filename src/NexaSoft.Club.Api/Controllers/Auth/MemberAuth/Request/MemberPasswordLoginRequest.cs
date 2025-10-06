namespace NexaSoft.Club.Api.Controllers.Auth.MemberAuth.Request;

public sealed record MemberPasswordLoginRequest
(
    string Dni,
    string Password,
    string? DeviceId,
    string? BiometricToken = null
);