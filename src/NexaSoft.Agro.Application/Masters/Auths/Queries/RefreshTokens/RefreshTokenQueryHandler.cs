using NexaSoft.Agro.Application.Abstractions.Auth;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Agro.Application.Masters.Auths.Queries.RefreshTokens;

public class RefreshTokenQueryHandler(IAuthService _authService) : IQueryHandler<RefreshTokenQuery, AuthTokenResponse>
{
    public async Task<Result<AuthTokenResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        return await _authService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
    }
}
