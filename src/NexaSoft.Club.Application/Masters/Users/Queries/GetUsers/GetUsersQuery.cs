using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Masters.Users.Queries.GetUsers;

public sealed record GetUsersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<UserResponse>>;
