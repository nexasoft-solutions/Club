using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Queries.GetTimeRequestType;

public sealed record GetTimeRequestTypeQuery(
    long Id
) : IQuery<TimeRequestTypeResponse>;
