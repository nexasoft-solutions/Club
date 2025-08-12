using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUsers;

public sealed record GetUsersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<UserResponse>>;
