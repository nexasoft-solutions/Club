using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.MemberFees.Commands.DeleteMemberFee;

public sealed record DeleteMemberFeeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
