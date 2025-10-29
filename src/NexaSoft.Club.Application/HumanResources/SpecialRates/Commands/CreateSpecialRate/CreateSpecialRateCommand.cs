using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.CreateSpecialRate;

public sealed record CreateSpecialRateCommand(
    long? RateTypeId,
    string? Name,
    decimal Multiplier,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string CreatedBy
) : ICommand<long>;
