using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Queries.GetTimeRequest;

public sealed record GetTimeRequestQuery(
    long Id
) : IQuery<TimeRequestResponse>;
