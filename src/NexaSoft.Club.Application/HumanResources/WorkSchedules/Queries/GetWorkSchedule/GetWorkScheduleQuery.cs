using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Queries.GetWorkSchedule;

public sealed record GetWorkScheduleQuery(
    long Id
) : IQuery<WorkScheduleResponse>;
