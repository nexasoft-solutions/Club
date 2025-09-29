using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.SystemUsers;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Queries.GetSystemUsers;

public sealed record GetSystemUsersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SystemUserResponse>>;
