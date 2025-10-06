namespace NexaSoft.Club.Api.Controllers.Auth.MemberAuth.Request;

public sealed record VerifyMemberPinRequest
(
    string Dni,
    DateOnly BirthDate,
    string Pin, 
    string DeviceId
);