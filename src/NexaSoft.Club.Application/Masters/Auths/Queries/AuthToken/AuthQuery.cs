using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Auths.Queries.AuthToken;

public sealed record AuthQuery(
    string UserName,
    string Password
):IQuery<AuthTokenResponse>;
