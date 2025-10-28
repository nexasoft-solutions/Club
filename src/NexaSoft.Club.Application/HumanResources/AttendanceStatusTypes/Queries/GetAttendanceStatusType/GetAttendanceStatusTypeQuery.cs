using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Queries.GetAttendanceStatusType;

public sealed record GetAttendanceStatusTypeQuery(
    long Id
) : IQuery<AttendanceStatusTypeResponse>;
