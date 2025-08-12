using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Agro.Application.Masters.Auths.Queries.RefreshTokens;

public sealed record RefreshTokenQuery(
    string RefreshToken
) : IQuery<AuthTokenResponse>;
