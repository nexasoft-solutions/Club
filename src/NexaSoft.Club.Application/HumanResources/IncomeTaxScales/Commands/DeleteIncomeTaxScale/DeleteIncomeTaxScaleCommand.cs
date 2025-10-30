using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.DeleteIncomeTaxScale;

public sealed record DeleteIncomeTaxScaleCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
