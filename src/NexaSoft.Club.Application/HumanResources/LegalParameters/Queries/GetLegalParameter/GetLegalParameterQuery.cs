using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Queries.GetLegalParameter;

public sealed record GetLegalParameterQuery(
    long Id
) : IQuery<LegalParameterResponse>;
