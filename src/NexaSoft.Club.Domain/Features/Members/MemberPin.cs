using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members;

public class MemberPin : Entity
{
    public long MemberId { get; private set; }
    public string Pin { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public string DeviceId { get; private set; } = string.Empty;
    public bool IsUsed { get; private set; }
    public DateTime? UsedAt { get; private set; }

    // Navigation property
    public virtual Member Member { get; private set; } = null!;

    private MemberPin() { }

    public static MemberPin Create(
        long memberId,
        string pin,
        DateTime expiresAt,
        string deviceId,
        DateTime createdAt,
        string createdBy)
    {
        return new MemberPin
        {
            MemberId = memberId,
            Pin = pin,
            ExpiresAt = expiresAt,
            DeviceId = deviceId,
            IsUsed = false,
            CreatedAt = createdAt,
            CreatedBy = createdBy
        };
    }

    public void MarkAsUsed()
    {
        IsUsed = true;
        UsedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsValid()
    {
        return !IsUsed && DateTime.UtcNow <= ExpiresAt;
    }
}
