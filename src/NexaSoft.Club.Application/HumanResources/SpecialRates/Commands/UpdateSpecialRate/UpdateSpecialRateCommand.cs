using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.UpdateSpecialRate;

public sealed record UpdateSpecialRateCommand(
    long Id,
    long? RateTypeId,
    string? Name,
    decimal Multiplier,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string UpdatedBy
) : ICommand<bool>;
