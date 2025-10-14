using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Statuses;

namespace NexaSoft.Club.Application.Masters.Statuses.Queries.GetStatus;

public sealed record GetStatusQuery(
    long Id
) : IQuery<StatusResponse>;
