using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetFeeConfigurations;

public sealed record GetFeeConfigurationsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<FeeConfigurationResponse>>;
