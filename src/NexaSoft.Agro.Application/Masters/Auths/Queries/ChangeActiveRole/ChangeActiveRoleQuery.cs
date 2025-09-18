using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Agro.Application.Masters.Auths.Queries.ChangeActiveRole;

public sealed record  ChangeActiveRoleQuery(long UserId, long NewRoleId) : IQuery<AuthTokenResponse>;
