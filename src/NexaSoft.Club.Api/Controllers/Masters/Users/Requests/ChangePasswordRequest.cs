namespace NexaSoft.Club.Api.Controllers.Masters.Users.Requests;

public sealed record ChangePasswordRequest
(
    long UserId,
    string NewPassword
);
