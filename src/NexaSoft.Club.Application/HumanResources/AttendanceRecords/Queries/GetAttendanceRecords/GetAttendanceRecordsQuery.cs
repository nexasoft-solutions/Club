using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Queries.GetAttendanceRecords;

public sealed record GetAttendanceRecordsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<AttendanceRecordResponse>>;
