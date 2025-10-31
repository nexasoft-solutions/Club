using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Queries.GetPayrollType;

public sealed record GetPayrollTypeQuery(
    long Id
) : IQuery<PayrollTypeResponse>;
