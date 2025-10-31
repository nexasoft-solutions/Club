using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Queries.GetPayrollTypes;

public sealed record GetPayrollTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollTypeResponse>>;
