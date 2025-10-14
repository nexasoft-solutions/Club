namespace NexaSoft.Club.Api.Controllers.Features.Members.Request;

public sealed record UpdateMemberRequest(
    long Id,
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
    decimal Balance,   
    string UpdatedBy
);
