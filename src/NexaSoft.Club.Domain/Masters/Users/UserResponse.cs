namespace NexaSoft.Club.Domain.Masters.Users;

public sealed record UserResponse(
    long Id,
    string? LastName,
    string? FirstName,
    string? FullName,
    string? UserName,
    string? Email,
    string? Dni,
    string? Phone,
    long UserTypeId,
    string? UserTypeName,
    DateOnly? BirthDate,
    string? ProfilePictureUrl,
    string? QrCode,
    DateOnly? QrExpiration,
    string? QrUrl,
    DateTime? LastLoginDate,
    DateTime? PasswordSetDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
