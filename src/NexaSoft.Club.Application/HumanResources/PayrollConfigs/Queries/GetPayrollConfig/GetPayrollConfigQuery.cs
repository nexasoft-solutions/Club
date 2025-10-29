using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Queries.GetPayrollConfig;

public sealed record GetPayrollConfigQuery(
    long Id
) : IQuery<PayrollConfigResponse>;
