using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.MemberFees.Commands.UpdateMemberFee;

public sealed record UpdateMemberFeeCommand(
    long Id,
    long MemberId,
    long? ConfigId,
    string? Period,
    decimal Amount,
    DateOnly DueDate,
    string? Status,
    string UpdatedBy
) : ICommand<bool>;
