using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.UpdateSpace;

public sealed record UpdateSpaceCommand(
    long Id,
    string? SpaceName,
    string? SpaceType,
    int? Capacity,
    string? Description,
    decimal StandardRate,
    bool IsActive,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string UpdatedBy
) : ICommand<bool>;
