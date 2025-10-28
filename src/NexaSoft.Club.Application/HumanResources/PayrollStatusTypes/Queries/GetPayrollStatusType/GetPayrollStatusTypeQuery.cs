using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Queries.GetPayrollStatusType;

public sealed record GetPayrollStatusTypeQuery(
    long Id
) : IQuery<PayrollStatusTypeResponse>;
