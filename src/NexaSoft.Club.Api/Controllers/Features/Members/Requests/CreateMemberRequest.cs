namespace NexaSoft.Club.Api.Controllers.Features.Members.Request;

public sealed record CreateMemberRequest(
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    long DepartamentId,
    long ProvinceId,
    long DistrictId,
    string? Address,
    DateOnly? BirthDate,
    long MemberTypeId,
    long StatusId,
    DateOnly JoinDate,
    DateOnly? ExpirationDate,
    decimal Balance,
    long UserTypeId,
    string CreatedBy
);
