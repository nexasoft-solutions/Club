using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Auths.Queries.AuthToken;

public class AuthQueryHandler(IAuthService _authService) : IQueryHandler<AuthQuery, AuthTokenResponse>
{
    public async Task<Result<AuthTokenResponse>> Handle(AuthQuery request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request.UserName, request.Password, cancellationToken);
    }
}
