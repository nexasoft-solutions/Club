using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.SystemUsers;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Queries.GetSystemUser;

public sealed record GetSystemUserQuery(
    long Id
) : IQuery<SystemUserResponse>;
