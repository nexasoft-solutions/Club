using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUser;

public sealed record GetUserQuery(
    Guid Id
) : IQuery<UserResponse>;
