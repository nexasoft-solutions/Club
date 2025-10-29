using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Queries.GetPayrollConfigs;

public sealed record GetPayrollConfigsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollConfigResponse>>;
