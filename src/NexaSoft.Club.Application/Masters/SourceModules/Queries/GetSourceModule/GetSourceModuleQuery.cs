using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.SourceModules;

namespace NexaSoft.Club.Application.Masters.SourceModules.Queries.GetSourceModule;

public sealed record GetSourceModuleQuery(
    long Id
) : IQuery<SourceModuleResponse>;
