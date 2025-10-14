using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.UpdateSpace;

public sealed record UpdateSpaceCommand(
    long Id,
    string SpaceName,
    long SpaceTypeId,
    int Capacity,
    string Description,
    decimal StandardRate,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string UpdatedBy
) : ICommand<bool>;
