using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Queries.GetPayrollStatusTypes;

public sealed record GetPayrollStatusTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollStatusTypeResponse>>;
