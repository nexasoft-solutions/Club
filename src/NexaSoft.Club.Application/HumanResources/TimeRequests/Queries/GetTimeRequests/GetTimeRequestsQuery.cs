using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Queries.GetTimeRequests;

public sealed record GetTimeRequestsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<TimeRequestResponse>>;
