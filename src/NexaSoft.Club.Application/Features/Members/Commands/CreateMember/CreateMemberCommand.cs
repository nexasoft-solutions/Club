using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Address,
    DateOnly? BirthDate,
    long MemberTypeId,
    long StatusId,
    DateOnly JoinDate,
    DateOnly? ExpirationDate,
    decimal Balance,
    string? QrCode,
    DateTime? QrExpiration,
    string? ProfilePictureUrl,   
    string CreatedBy
) : ICommand<long>;
