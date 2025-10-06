namespace NexaSoft.Club.Api.Controllers.Auth.MemberAuth.Request;

public sealed record MemberRegistrtionRequest
(
    string Dni,
    DateOnly BirthDate,
    string DeviceId,
    string CreatedBy
);