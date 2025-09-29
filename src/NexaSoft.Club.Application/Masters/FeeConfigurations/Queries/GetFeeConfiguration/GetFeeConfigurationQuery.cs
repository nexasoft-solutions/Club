using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetFeeConfiguration;

public sealed record GetFeeConfigurationQuery(
    long Id
) : IQuery<FeeConfigurationResponse>;
