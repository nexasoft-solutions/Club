using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Queries.GetAttendanceStatusTypes;

public sealed record GetAttendanceStatusTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<AttendanceStatusTypeResponse>>;
