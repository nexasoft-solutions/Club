namespace NexaSoft.Club.Api.Controllers.Features.Members.Requests;

public sealed record  HasPasswordRequest
(
    string Dni,
    DateOnly BirthDate
);
