using NexaSoft.Agro.Application.Abstractions.Auth;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Agro.Application.Masters.Auths.Queries.ChangeActiveRole;

public class ChangeActiveRoleQueryHandler(
    IAuthService _authService
) : IQueryHandler<ChangeActiveRoleQuery, AuthTokenResponse>
{
    public async Task<Result<AuthTokenResponse>> Handle(ChangeActiveRoleQuery request, CancellationToken cancellationToken)
    {
        return await _authService.ChangeActiveRoleAsync(request.UserId, request.NewRoleId, cancellationToken);
    }
}
