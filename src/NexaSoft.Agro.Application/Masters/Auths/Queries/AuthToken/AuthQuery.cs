using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;

public sealed record AuthQuery(
    string UserName,
    string Password
):IQuery<AuthTokenResponse>;
