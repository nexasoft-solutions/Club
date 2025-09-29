using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Auth;

public class RefreshToken : Entity
{
    public string? Token { get; private set; }
    public long UserId { get; private set; }
    public DateTime Expires { get; private set; }
    public bool Revoked { get; private set; }

    private RefreshToken() { }

    private RefreshToken(
        string token,
        long userId,
        DateTime expires,
        DateTime createdAt,
        string createdBy
    ) : base(createdAt, createdBy)
    {
        Token = token;
        UserId = userId;
        Expires = expires;
        Revoked = false;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public static RefreshToken Create(
        string token,
        long userId,
        DateTime expires,
        string createdBy
    )
    {
        return new RefreshToken(
            token,
            userId,
            expires,
            DateTime.UtcNow,
            createdBy
        );
    }

    public void Revoke()
    {
        Revoked = true;
    }

    public bool IsActive()
    {
        return !Revoked && DateTime.UtcNow < Expires;
    }
}