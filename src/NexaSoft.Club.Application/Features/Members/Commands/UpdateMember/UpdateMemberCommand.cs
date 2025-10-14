using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Commands.UpdateMember;

public sealed record UpdateMemberCommand(
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
) : ICommand<bool>;
