using NexaSoft.Club.Application.Masters.Auths.Queries.AuthToken;
using NexaSoft.Club.Domain.Auth;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Abstractions.Auth;

public interface IAuthService
{
    Task<Result<AuthTokenResponse>> Login(string userName, string password, CancellationToken cancellationToken);
    Task<Result<User?>> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<Result<User?>> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<Result<RefreshToken>> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

    Task<Result<AuthTokenResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

    Task<string> GenerateAccessToken(User user, long activeRoleId, CancellationToken cancellationToken);
    
    Task<Result<AuthTokenResponse>> ChangeActiveRoleAsync(long userId, long newRoleId, CancellationToken cancellationToken);
}
