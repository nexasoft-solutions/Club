using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Commands.CreateSpaceRate;

public sealed record CreateSpaceRateCommand(
    long SpaceId,
    long MemberTypeId,
    decimal Rate,
    bool IsActive,
    string CreatedBy
) : ICommand<long>;
