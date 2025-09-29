using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Masters.Auths.Queries.AuthToken;

namespace NexaSoft.Club.Application.Masters.Auths.Queries.ChangeActiveRole;

public class ChangeActiveRoleQueryHandler(
    IAuthService _authService
) : IQueryHandler<ChangeActiveRoleQuery, AuthTokenResponse>
{
    public async Task<Result<AuthTokenResponse>> Handle(ChangeActiveRoleQuery request, CancellationToken cancellationToken)
    {
        return await _authService.ChangeActiveRoleAsync(request.UserId, request.NewRoleId, cancellationToken);
    }
}
