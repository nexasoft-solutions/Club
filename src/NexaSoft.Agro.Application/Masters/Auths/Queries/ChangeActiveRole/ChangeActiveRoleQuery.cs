using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Agro.Application.Masters.Auths.Queries.ChangeActiveRole;

public sealed record  ChangeActiveRoleQuery(Guid UserId, Guid NewRoleId) : IQuery<AuthTokenResponse>;
