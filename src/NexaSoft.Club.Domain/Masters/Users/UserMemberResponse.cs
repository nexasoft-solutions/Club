namespace NexaSoft.Club.Domain.Masters.Users;

public sealed record UserMemberResponse
(
    long MemberId,
    long UserId,
    string? QrCode,
    DateOnly? QrExpiration,
    string? QrUrl,
    string? CreatedBy

);
