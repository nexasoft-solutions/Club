using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.MemberFees.Commands.CreateMemberFee;

public sealed record CreateMemberFeeCommand(
    long MemberId,
    long? ConfigId,
    string? Period,
    decimal Amount,
    DateOnly DueDate,
    long StatusId,
    string CreatedBy
) : ICommand<long>;
