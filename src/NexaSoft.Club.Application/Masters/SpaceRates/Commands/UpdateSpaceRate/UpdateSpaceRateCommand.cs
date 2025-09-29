using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Commands.UpdateSpaceRate;

public sealed record UpdateSpaceRateCommand(
    long Id,
    long SpaceId,
    long MemberTypeId,
    decimal Rate,
    bool IsActive,
    string UpdatedBy
) : ICommand<bool>;
