using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.MemberTypes;
using NexaSoft.Club.Domain.Masters.MemberStatuses;
using NexaSoft.Club.Domain.Masters.Statuses;
using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Domain.Features.Members;

public class Member : Entity
{
    public string? Dni { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }

    public long DepartamentId { get; private set; }
    public Ubigeo? Departament { get; private set; }
    public long ProvinceId { get; private set; }
    public Ubigeo? Province { get; private set; }
    public long DistrictId { get; private set; }
    public Ubigeo? District { get; private set; }
    public string? Address { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public long MemberTypeId { get; private set; }
    public MemberType? MemberType { get; private set; }
    public long MemberStatusId { get; private set; }
    public MemberStatus? MemberStatus { get; private set; }
    public DateOnly JoinDate { get; private set; }
    public DateOnly? ExpirationDate { get; private set; }
    public decimal Balance { get; private set; }
     public int StateMember { get; private set; }
    public bool EntryFeePaid { get; private set; }
    public DateTime? LastPaymentDate { get; private set; }
    public long? StatusId { get; private set; }
    public Status? Status { get; private set; }
   //Para Login
    public string? PasswordHash { get; private set; }

    //private readonly List<MemberQrHistory> _qrHistory = new();
    //public IReadOnlyCollection<MemberQrHistory> QrHistory => _qrHistory.AsReadOnly();

    private Member() { }

    private Member(
        string? dni,
        string? firstName,
        string? lastName,
        string? email,
        string? phone,
        long departamentId,
        long provinceId,    
        long districtId,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long memberStatusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        int stateMember,
        bool entryFeePaid,
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
        DepartamentId = departamentId;
        ProvinceId = provinceId;
        DistrictId = districtId;
        Address = address;
        BirthDate = birthDate;
        MemberTypeId = memberTypeId;
        MemberStatusId = memberStatusId;
        JoinDate = joinDate;
        ExpirationDate = expirationDate;
        Balance = balance;
        StateMember = stateMember;
        EntryFeePaid = entryFeePaid;
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
        long departamentId,
        long provinceId,
        long districtId,
        string? address,
        DateOnly? birthDate,
        long memberTypeId,
        long statusId,
        DateOnly joinDate,
        DateOnly? expirationDate,
        decimal balance,
        int stateMember,
        bool entryFeePaid,
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
            departamentId,
            provinceId, 
            districtId,
            address,
            birthDate,
            memberTypeId,
            statusId,
            joinDate,
            expirationDate,
            balance,
            stateMember,
            entryFeePaid,
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
        long departamentId,
        long provinceId,
        long districtId,
        string? address,
        DateOnly? birthDate,    
        decimal balance,
        DateTime utcNow,
        string? updatedBy
    )
    {
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        DepartamentId = departamentId;
        ProvinceId = provinceId;
        DistrictId = districtId;
        Address = address;
        BirthDate = birthDate;
        Balance = balance;
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

    public void RevertBalance(decimal amount, string updatedBy)
    {
        // Revertir la operación - asumiendo que UpdateBalance resta del balance
        Balance += amount; // Si UpdateBalance suma, entonces sería Balance -= amount
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;

        // Log para auditoría
        /*_logger?.LogInformation("Balance revertido para miembro {MemberId}. Monto: {Amount}, Razón: {Reason}", 
            Id, amount, reason);*/
    }

    public void MarkAsCompleted()
    {
        //SetAccountingEntryId(accountingEntryId);
        StatusId = (long)StatusEnum.Completado;
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
        StatusId = (long)StatusEnum.Iniciado;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFeesGenerationCompleted()
    {
        // Status = "Active";
        StatusId = (long)StatusEnum.Completado;
        UpdatedAt = DateTime.UtcNow;
        // Log para auditoría
        //_logger?.LogInformation("Generación de cuotas completada para member {MemberId}", Id);
    }

    public void MarkAsFeesGenerationFailed()
    {
        StatusId = (long)StatusEnum.Fallido;
        UpdatedAt = DateTime.UtcNow;

        // Podrías agregar un campo ErrorMessage
        //_logger?.LogWarning("Generación de cuotas falló para member {MemberId}: {Error}", Id, error);
    }

    public void MarkAsActive()
    {
        StatusId = (long)StatusEnum.Activo;
        UpdatedAt = DateTime.UtcNow;

        // Podrías agregar un campo ErrorMessage
        //_logger?.LogWarning("Generación de cuotas falló para member {MemberId}: {Error}", Id, error);
    }

    // Método para verificar si necesita nuevo QR
    /*public bool NeedsNewQr()
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
                createdAt
            );
            _qrHistory.Add(qrHistory);
        }

        // Actualizar QR actual
        QrCode = newQrCode;
        QrUrl = qrUrl;
        QrExpiration = expirationDate;
        UpdatedAt = DateTime.UtcNow;
    }*/

    public bool IsUpToDateForQrRenewal()
    {
        // Lógica personalizada - ejemplo simple
        return Balance >= 0;
    }
  
}
