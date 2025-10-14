using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Users;

public class UserQrHistory: Entity
{
    public long UserId { get; private set; }
    public User? User { get; private set; }
    public string QrCode { get; private set; } = string.Empty;
    public string QrUrl { get; private set; } = string.Empty;
    public DateOnly? ExpirationDate { get; private set; }  
    // Navigation property

    private UserQrHistory() { }

    public UserQrHistory(
        long userId,
        string qrCode,
        string qrUrl,
        DateOnly? expirationDate,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        UserId = userId;
        QrCode = qrCode;
        QrUrl = qrUrl;
        ExpirationDate = expirationDate;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static UserQrHistory Create(
        long userId,
        string qrCode,
        string qrUrl,
        DateOnly? expirationDate,
        DateTime createdAt,
        string createdBy)
    {
        return new UserQrHistory
        {
            UserId = userId,
            QrCode = qrCode,
            QrUrl = qrUrl,
            ExpirationDate = expirationDate,
            CreatedAt = createdAt,
            CreatedBy = createdBy
        };
    }
}
