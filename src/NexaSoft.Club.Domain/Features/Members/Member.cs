using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.MemberTypes;
using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Domain.Features.Members;

public class Member : Entity
{
    public string? Dni { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public long MemberTypeId { get; private set; }
    public MemberType? MemberType { get; private set; }
    public long MemberStatusId { get; private set; }
    public MemberStatus? MemberStatus { get; private set; }
    public DateOnly JoinDate { get; private set; }
    public DateOnly? ExpirationDate { get; private set; }
    public decimal Balance { get; private set; }
    public string? QrCode { get; private set; }
    public DateOnly? QrExpiration { get; private set; }
    public string? QrUrl { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public int StateMember { get; private set; }
    public bool EntryFeePaid { get; private set; }
    public DateTime? LastPaymentDate { get; private set; }
    public string? Status { get; private set; }

    //Para Login
    public string? PasswordHash { get; private set; }
    public string? BiometricToken { get; private set; }
    public DateTime? PasswordSetDate { get; private set; }
    public DateTime? LastLoginDate { get; private set; }
    public string? DeviceId { get; private set; }
    public bool HasSetPassword { get; private set; } // ← NUEVO: saber si ya configuró password

    private readonly List<MemberQrHistory> _qrHistory = new();
    public IReadOnlyCollection<MemberQrHistory> QrHistory => _qrHistory.AsReadOnly();

    private Member() { }

    private Member(
        string? dni,
        string? firstName,
        string? lastName,
        string? email,
        string? phone,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long memberStatusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        //string? qrCode,
        //DateTime? qrExpiration,
        string? profilePictureUrl,
        int stateMember,
        bool entryFeePaid,
        DateTime? lastPaymentDate,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        BirthDate = birthDate;
        MemberTypeId = memberTypeId;
        MemberStatusId = memberStatusId;
        JoinDate = joinDate;
        ExpirationDate = expirationDate;
        Balance = balance;
        //QrCode = qrCode;
        //QrExpiration = qrExpiration;
        ProfilePictureUrl = profilePictureUrl;
        StateMember = stateMember;
        EntryFeePaid = entryFeePaid;
        LastPaymentDate = lastPaymentDate;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Member Create(
        string? dni,
        string? firstName,
        string? lastName,
        string? email,
        string? phone,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long statusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        //string? qrCode,
        //DateTime? qrExpiration,
        string? profilePictureUrl,
        int stateMember,
        bool entryFeePaid,
        DateTime? lastPaymentDate,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Member(
            dni,
            firstName,
            lastName,
            email,
            phone,
            address,
            birthDate,
            memberTypeId,
            statusId,
            joinDate,
            expirationDate,
            balance,
            //qrCode,
            //qrExpiration,
            profilePictureUrl,
            stateMember,
            entryFeePaid,
            lastPaymentDate,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? dni,
        string? firstName,
        string? lastName,
        string? email,
        string? phone,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long statusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        //string? qrCode,
        //DateTime? qrExpiration,
        string? profilePictureUrl,
        bool entryFeePaid,
        DateTime? lastPaymentDate,
        DateTime utcNow,
        string? updatedBy
    )
    {
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        BirthDate = birthDate;
        MemberTypeId = memberTypeId;
        MemberStatusId = statusId;
        JoinDate = joinDate;
        ExpirationDate = expirationDate;
        Balance = balance;
        //QrCode = qrCode;
        //QrExpiration = qrExpiration;
        ProfilePictureUrl = profilePictureUrl;
        EntryFeePaid = entryFeePaid;
        LastPaymentDate = lastPaymentDate;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateMember = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    public void UpdateBalance(decimal paymentAmount, string updatedBy)
    {
        Balance += paymentAmount; // Aumentar balance (si era negativo, se reduce la deuda)
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    public void RevertBalance(decimal amount)
    {
        // Revertir la operación - asumiendo que UpdateBalance resta del balance
        Balance += amount; // Si UpdateBalance suma, entonces sería Balance -= amount
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = "System";

        // Log para auditoría
        /*_logger?.LogInformation("Balance revertido para miembro {MemberId}. Monto: {Amount}, Razón: {Reason}", 
            Id, amount, reason);*/
    }

    public void MarkAsCompleted()
    {
        //SetAccountingEntryId(accountingEntryId);
        Status = "Completed";
        UpdatedAt = DateTime.UtcNow;
        // Puedes agregar más lógica de estado si es necesario
    }

    /*public void MarkAsFailed()
    {
        Status = "Failed";
        //ErrorMessage = error;
        UpdatedAt = DateTime.UtcNow;
    }*/

    public void MarkAsProcessing()
    {
        Status = "Processing";
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFeesGenerationCompleted()
    {
        // Status = "Active";
        Status = "Completed";
        UpdatedAt = DateTime.UtcNow;
        // Log para auditoría
        //_logger?.LogInformation("Generación de cuotas completada para member {MemberId}", Id);
    }

    public void MarkAsFeesGenerationFailed()
    {
        Status = "Failed";
        UpdatedAt = DateTime.UtcNow;

        // Podrías agregar un campo ErrorMessage
        //_logger?.LogWarning("Generación de cuotas falló para member {MemberId}: {Error}", Id, error);
    }

    // Método para verificar si necesita nuevo QR
    public bool NeedsNewQr()
    {
        if (string.IsNullOrEmpty(QrCode) || QrExpiration == null)
            return true;

        return QrExpiration.Value <= DateOnly.FromDateTime(DateTime.Now.AddDays(15));
    }

    public void UpdateQr(string newQrCode, string qrUrl, DateOnly expirationDate, string createdAt)
    {
        // Guardar en historial si ya existe un QR
        if (!string.IsNullOrEmpty(QrCode))
        {
            var qrHistory = MemberQrHistory.Create(
                Id,
                QrCode,
                QrUrl ?? string.Empty,
                QrExpiration,
                DateTime.UtcNow,
                "System"
            );
            _qrHistory.Add(qrHistory);
        }

        // Actualizar QR actual
        QrCode = newQrCode;
        QrUrl = qrUrl;
        QrExpiration = expirationDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsUpToDateForQrRenewal()
    {
        // Lógica personalizada - ejemplo simple
        return Balance >= 0;
    }
    

    // Métodos
    public void SetPassword(string password)
    {
        PasswordHash = password;
        HasSetPassword = true;
        PasswordSetDate = DateTime.UtcNow;
    }

    /*public bool VerifyPassword(string password)
    {
        return PasswordHash != null && BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }*/

    public void SetBiometricToken(string token)
    {
        BiometricToken = token;
    }

    public void RecordLogin(string deviceId)
    {
        LastLoginDate = DateTime.UtcNow;
        DeviceId = deviceId;
    }

    public bool CanUsePasswordAuth() => HasSetPassword && PasswordHash != null;
}
