using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.SourceModules;

namespace NexaSoft.Club.Application.Masters.SourceModules.Queries.GetSourceModules;

public sealed record GetSourceModulesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SourceModuleResponse>>;
