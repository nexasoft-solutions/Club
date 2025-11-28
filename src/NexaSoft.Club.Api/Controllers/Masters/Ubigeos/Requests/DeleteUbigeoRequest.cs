namespace NexaSoft.Club.Api.Controllers.Masters.Ubigeos.Requests;

public sealed record DeleteUbigeoRequest
(
    long Id,
    string DeletedBy
);