using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Club.Application.Masters.Auths.Queries.RefreshTokens;

public sealed record RefreshTokenQuery(
    string RefreshToken
) : IQuery<AuthTokenResponse>;
