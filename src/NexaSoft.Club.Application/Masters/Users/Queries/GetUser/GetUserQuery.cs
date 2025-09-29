using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Masters.Users.Queries.GetUser;

public sealed record GetUserQuery(
    long Id
) : IQuery<UserResponse>;
