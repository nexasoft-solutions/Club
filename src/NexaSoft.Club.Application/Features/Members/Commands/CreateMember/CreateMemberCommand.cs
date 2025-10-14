using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(
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
    long userTypeId,
    string CreatedBy
) : ICommand<long>;


public record MemberFeesBackgroundData(
    long MemberId,
    long MemberTypeId,
    DateOnly JoinDate,
    DateOnly? ExpirationDate,
    long userTypeId,
    string CreatedBy
);