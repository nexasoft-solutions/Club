using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.UserTypes;

namespace NexaSoft.Club.Application.Masters.UserTypes.Queries.GetUserTypes;

public sealed record GetUserTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<UserTypeResponse>>;
