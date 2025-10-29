using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Queries.GetWorkSchedules;

public sealed record GetWorkSchedulesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<WorkScheduleResponse>>;
