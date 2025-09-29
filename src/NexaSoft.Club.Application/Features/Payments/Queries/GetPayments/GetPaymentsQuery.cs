using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Application.Features.Payments.Queries.GetPayments;

public sealed record GetPaymentsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PaymentResponse>>;
