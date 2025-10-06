using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members;

public class MemberQrHistory: Entity
{
    public long MemberId { get; private set; }
    public string QrCode { get; private set; } = string.Empty;
    public string QrUrl { get; private set; } = string.Empty;
    public DateOnly? ExpirationDate { get; private set; }  
    // Navigation property
    public virtual Member Member { get; private set; } = null!;

    private MemberQrHistory() { }

    public MemberQrHistory(
        long memberId,
        string qrCode,
        string qrUrl,
        DateOnly? expirationDate,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        MemberId = memberId;
        QrCode = qrCode;
        QrUrl = qrUrl;
        ExpirationDate = expirationDate;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static MemberQrHistory Create(
        long memberId,
        string qrCode,
        string qrUrl,
        DateOnly? expirationDate,
        DateTime createdAt,
        string createdBy)
    {
        return new MemberQrHistory
        {
            MemberId = memberId,
            QrCode = qrCode,
            QrUrl = qrUrl,
            ExpirationDate = expirationDate,
            CreatedAt = createdAt,
            CreatedBy = createdBy
        };
    }
}
