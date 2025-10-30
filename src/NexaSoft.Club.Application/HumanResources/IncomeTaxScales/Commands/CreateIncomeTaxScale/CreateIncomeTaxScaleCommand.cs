using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.CreateIncomeTaxScale;

public sealed record CreateIncomeTaxScaleCommand(
    int ScaleYear,
    decimal MinIncome,
    decimal? MaxIncome,
    decimal FixedAmount,
    decimal Rate,
    decimal ExcessOver,
    string? Description,
    string CreatedBy
) : ICommand<long>;
