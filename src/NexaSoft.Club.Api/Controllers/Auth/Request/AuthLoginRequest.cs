namespace NexaSoft.Club.Api.Controllers.Auth.request;

public sealed record AuthLoginRequest(
    string UserName,
    string Password
);

