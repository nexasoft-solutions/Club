using NexaSoft.Agro.Application.Abstractions.Auth;
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;

public class AuthQueryHandler(IAuthService _authService) : IQueryHandler<AuthQuery, AuthTokenResponse>
{
    public async Task<Result<AuthTokenResponse>> Handle(AuthQuery request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request.UserName, request.Password, cancellationToken);
    }
}
