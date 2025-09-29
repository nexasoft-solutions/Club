using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Club.Application.Masters.Auths.Queries.ChangeActiveRole;

public sealed record  ChangeActiveRoleQuery(long UserId, long NewRoleId) : IQuery<AuthTokenResponse>;
