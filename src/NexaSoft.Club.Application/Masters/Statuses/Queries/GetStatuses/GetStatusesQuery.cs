using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Statuses;

namespace NexaSoft.Club.Application.Masters.Statuses.Queries.GetStatuses;

public sealed record GetStatusesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<StatusResponse>>;
