using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Commands.InitMemberRegistration;

public sealed record InitMemberRegistrationCommand(
    string Dni,
    DateOnly BirthDate,
    string DeviceId,
    string CreatedBy
) : ICommand<MemberRegistrationResponse>;

public sealed record MemberRegistrationResponse(
    bool Success,
    string Message,
    string? MemberName = null,
    string? Email = null,
    int PinExpirationMinutes = 10
);
