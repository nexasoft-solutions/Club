using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Queries.GetTimeRequestTypes;

public sealed record GetTimeRequestTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<TimeRequestTypeResponse>>;
