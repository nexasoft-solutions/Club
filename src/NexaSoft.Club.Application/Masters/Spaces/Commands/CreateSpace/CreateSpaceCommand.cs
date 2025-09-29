using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.CreateSpace;

public sealed record CreateSpaceCommand(
    string? SpaceName,
    string? SpaceType,
    int? Capacity,
    string? Description,
    decimal StandardRate,
    bool IsActive,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string CreatedBy
) : ICommand<long>;
