namespace NexaSoft.Club.Domain.Auth;

public sealed record MemberTokenResponse
(
    string Token,
    string RefreshToken,
    DateTime ExpiresAt
);
