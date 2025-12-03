
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Users.Commands.ChangePassword;

public sealed record ChangePasswordCommand
(
    long UserId,
    string NewPassword  
): ICommand<bool>;
