using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members;

public class MemberRefreshToken: Entity
{
   public string Token { get; private set; } = string.Empty;
    public long MemberId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    // Navigation property
    public virtual Member Member { get; private set; } = null!;

    private MemberRefreshToken() { }

    public static MemberRefreshToken Create(string token, long memberId, DateTime expiresAt, string createdBy)
    {
        return new MemberRefreshToken
        {
            Token = token,
            MemberId = memberId,
            ExpiresAt = expiresAt,
            CreatedBy = createdBy,
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Revoke()
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsActive()
    {
        return !IsRevoked && !DeletedAt.HasValue && DateTime.UtcNow <= ExpiresAt;
    }
}
