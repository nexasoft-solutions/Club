using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Application.Features.Members.Commands.SetMemberPassword;


public record class SetMemberPasswordCommand(
    long UserId,
    string Password,
    string? BiometricToken, // Para guardar huella/face ID
    string DeviceId
) : ICommand<bool>;
