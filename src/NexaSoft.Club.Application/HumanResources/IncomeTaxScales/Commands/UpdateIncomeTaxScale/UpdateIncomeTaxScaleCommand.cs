using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.UpdateIncomeTaxScale;

public sealed record UpdateIncomeTaxScaleCommand(
    long Id,
    int ScaleYear,
    decimal MinIncome,
    decimal? MaxIncome,
    decimal FixedAmount,
    decimal Rate,
    decimal ExcessOver,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
