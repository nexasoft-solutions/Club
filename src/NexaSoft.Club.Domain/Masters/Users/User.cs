using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.UserTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Users;

public class User : Entity
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? FullName { get; private set; }
    public string? UserName { get; private set; }
    public bool HasSetPassword { get; set; }
    public string? Password { get; private set; }
    public string? Email { get; private set; }
    public string? Dni { get; private set; }
    public string? Phone { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public int UserStatus { get; private set; }
    public DateTime? PasswordSetDate { get; private set; }
    public DateTime? LastLoginDate { get; private set; }
    public string? BiometricToken { get; private set; }
 
    public string? DeviceId { get; private set; }
    public string? QrCode { get; private set; }
    public DateOnly? QrExpiration { get; private set; }
    public string? QrUrl { get; private set; }
    public string? ProfilePictureUrl { get; private set; }

    public long? MemberId { get; private set; }

    public Member? Member { get; private set; }

    public long UserTypeId { get; private set; }
    public UserType? UserType { get; set; }
    private readonly List<UserQrHistory> _qrHistory = new();
    public IReadOnlyCollection<UserQrHistory> QrHistory => _qrHistory.AsReadOnly();

    private User() { }

    private User(
        string? firstName,
        string? lastName,
        string? fullName,
        string? userName,
        string? email,
        string? dni,
        string? phone,
        long userTypeId,
        DateOnly? birthDate,
        long? memberId,
        int userStatus,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
        UserName = userName;
        Email = email;
        Dni = dni;
        Phone = phone;
        UserTypeId = userTypeId;
        BirthDate = birthDate;
        UserStatus = userStatus;
        MemberId = memberId;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static User Create(
        string? firstName,
        string? lastName,
        string? email,
        string? dni,
        string? phone,
        long userTypeId,
        DateOnly? birthDate,
        long? memberId,
        int userStatus,
        DateTime createdAt,
        string? createdBy
    )
    {
        var fullName = UserService.CreateFullName(firstName ?? "", lastName ?? "");
        var userName = UserService.CreateUserName(lastName ?? "", firstName ?? "");
        var entity = new User(
            firstName,
            lastName,
            fullName,
            userName,
            email,
            dni,
            phone,
            userTypeId,
            birthDate,
            memberId,
            userStatus,
            createdAt,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? firstName,
        string? lastName,
        string? email,
        string? dni,
        string? phone,
        long userTypeId,
        DateOnly? birthDate,
        long? memberId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        FirstName = firstName;
        LastName = lastName;

        // Actualiza NombreCompleto y UserName automáticamente
        FullName = UserService.CreateFullName(FirstName ?? "", LastName ?? "");
        UserName = UserService.CreateUserName(LastName ?? "", FirstName ?? "");

        Email = email;
        Dni = dni;
        Phone = phone;        
        UserTypeId = userTypeId;
        BirthDate = birthDate;
        MemberId = memberId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        UserStatus = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    public void SetPassword(string password)
    {
        Password = password;
        HasSetPassword = true;
        PasswordSetDate = DateTime.UtcNow;
    }

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
            var qrHistory = UserQrHistory.Create(
                Id,
                QrCode,
                QrUrl ?? string.Empty,
                QrExpiration,
                DateTime.UtcNow,
                createdAt
            );
            _qrHistory.Add(qrHistory);
        }

        // Actualizar QR actual
        QrCode = newQrCode;
        QrUrl = qrUrl;
        QrExpiration = expirationDate;
        UpdatedAt = DateTime.UtcNow;
    }


    public void UpdateLastLoginDate()
    {
        LastLoginDate = DateTime.UtcNow;
    }

    public bool CanUsePasswordAuth() => HasSetPassword && Password != null;


   
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

}
